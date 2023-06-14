using System;
using System.Collections.Generic;
using HKSH.Common.Caching.Redis;
using HKSH.Common.Constants;
using HKSH.Common.Extensions;
using HKSH.Common.Repository.Database;
using HKSH.Common.Repository.Database.Privileges;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace HKSH.Common.Context
{
    /// <summary>
    /// CurrentContext
    /// </summary>
    /// <seealso cref="ICurrentContext" />
    /// <seealso cref="IRepositoryCurrentContext" />
    public class CurrentContext : ICurrentContext, IRepositoryCurrentContext
    {
        /// <summary>
        /// http context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// redis repo
        /// </summary>
        private readonly IRedisRepository _redisRepository;

        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogger<CurrentContext> _logger;

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        private ClaimCurrentUser? _currentUser;

        /// <summary>
        /// current user id
        /// </summary>
        private long? _currentUserId;

        /// <summary>
        /// user permissions
        /// </summary>
        private List<UserPrivilegeModule> _userPermissions = new();

        /// <summary>
        /// user current location
        /// </summary>
        private string? _currentLocation;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="redisRepository"></param>
        /// <param name="logger"></param>
        public CurrentContext(IHttpContextAccessor httpContextAccessor, IRedisRepository redisRepository,
            ILogger<CurrentContext> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _redisRepository = redisRepository;
            _logger = logger;
        }

        /// <summary>
        /// current user id
        /// </summary>
        public long CurrentUserId
        {
            get
            {
                if (_currentUserId != null)
                {
                    return _currentUserId.Value;
                }

                var reallyUserId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(reallyUserId))
                {
                    var containsUserId = _httpContextAccessor?.HttpContext?.Request.Headers.ContainsKey(GlobalConstant.CURRENT_USER_CODE) ?? false;
                    if (containsUserId)
                    {
                        reallyUserId = _httpContextAccessor?.HttpContext?.Request.Headers[GlobalConstant.CURRENT_USER_CODE];
                    }
                }

                if (string.IsNullOrEmpty(reallyUserId))
                {
                    throw new Exception("Error access token");
                }

                int providerIndex = reallyUserId.IndexOf(':', 2);
                string externalId = reallyUserId.Substring(providerIndex + 1);

                if (!long.TryParse(externalId, out var userId))
                {
                    throw new Exception("Error access token");
                }

                _currentUserId = userId;

                return _currentUserId.Value;
            }
        }

        /// <summary>
        /// current user
        /// </summary>
        public ClaimCurrentUser CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }

                long userId = CurrentUserId;

                try
                {
                    var claimCurrentUser = _redisRepository.HashGet<ClaimCurrentUser>(ContextConst.KEY_PATTERN + userId, ContextConst.USER_INFO);
                    if (claimCurrentUser == null || claimCurrentUser.Id != userId)
                    {
                        throw new Exception("You are not logged in yet");
                    }

                    _currentUser = claimCurrentUser;
                    _currentUser.Permissions = UserPermissions;

                    return _currentUser;
                }
                catch (Exception e)
                {
                    _logger.LogExc(e, "Get user info from cache err ");
                    throw e;
                }
            }
        }

        /// <summary>
        /// current location
        /// </summary>
        public string CurrentLocation
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLocation))
                {
                    return _currentLocation;
                }

                long userId = CurrentUserId;

                try
                {
                    _currentLocation = _redisRepository.HashGet<string>(ContextConst.KEY_PATTERN + userId, ContextConst.LOCATION_INFO);
                    return _currentLocation;
                }
                catch (Exception e)
                {
                    _logger.LogExc(e, "Get user location info from cache err ");
                    throw e;
                }
            }
        }

        /// <summary>
        /// get user permissions
        /// </summary>
        public List<UserPrivilegeModule> UserPermissions
        {
            get
            {
                if (_userPermissions.Count > 0)
                {
                    return _userPermissions;
                }

                long userId = CurrentUserId;

                try
                {
                    _userPermissions = _redisRepository.HashGet<List<UserPrivilegeModule>>(ContextConst.KEY_PATTERN + userId, ContextConst.PRIVILEGE_INFO);
                    return _userPermissions;
                }
                catch (Exception e)
                {
                    _logger.LogExc(e, "Get user privilege info from cache err ");
                    throw e;
                }
            }
        }
    }
}