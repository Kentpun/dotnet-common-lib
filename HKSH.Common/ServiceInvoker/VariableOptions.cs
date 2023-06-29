﻿namespace HKSH.Common.ServiceInvoker;

/// <summary>
/// VariableOptions
/// </summary>
public class VariableOptions
{
    /// <summary>
    /// The section
    /// </summary>
    public const string Section = "VariableOptions";

    /// <summary>
    /// The organization daas host
    /// </summary>
    public const string OrganizationDaasHost = "OrganizationDaas";

    /// <summary>
    /// The patient daas host
    /// </summary>
    public const string PatientDaasHost = "PatientDaas";

    /// <summary>
    /// The acm master core host
    /// </summary>
    public const string AcmMasterCoreHost = "AcmMasterCore";

    /// <summary>
    /// The e form master core host
    /// </summary>
    public const string EFormMasterCoreHost = "MasterCore";

    /// <summary>
    /// The MDM master core host
    /// </summary>
    public const string MdmMasterCoreHost = "MdmMasterCore";

    /// <summary>
    /// The alert master core host
    /// </summary>
    public const string AlertMasterCoreHost = "AlertMasterCore";

    /// <summary>
    /// The alert master core host
    /// </summary>
    public const string NotificationMasterCoreHost = "NotificationMasterCore";

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>
    /// The key.
    /// </value>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public string Value { get; set; } = string.Empty;
}