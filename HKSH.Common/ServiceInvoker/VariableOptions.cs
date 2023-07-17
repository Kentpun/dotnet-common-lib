namespace HKSH.Common.ServiceInvoker;

/// <summary>
/// VariableOptions
/// </summary>
public class VariableOptions
{
    /// <summary>
    /// The section
    /// </summary>
    public const string SECTION = "VariableOptions";

    /// <summary>
    /// The acm master core host
    /// </summary>
    public const string ACM_MASTER_CORE_HOST = "AcmMasterCore";

    /// <summary>
    /// The e form master core host
    /// </summary>
    public const string EFORM_MASTER_CORE_HOST = "EFormMasterCore";

    /// <summary>
    /// The MDM master core host
    /// </summary>
    public const string MDM_MASTER_CORE_HOST = "MdmMasterCore";

    /// <summary>
    /// The alert master core host
    /// </summary>
    public const string ALERT_MASTER_CORE_HOST = "AlertMasterCore";

    /// <summary>
    /// The alert master core host
    /// </summary>
    public const string NOTIFICATION_MASTER_CORE_HOST = "NotificationMasterCore";

    /// <summary>
    /// The notification inbox function host
    /// </summary>
    public const string NOTIFICATION_INBOX_FUNCTION_HOST = "NotificationInboxFunction";

    /// <summary>
    /// The notification SMS function host
    /// </summary>
    public const string NOTIFICATION_SMS_FUNCTION_HOST = "NotificationSmsFunction";

    /// <summary>
    /// The notification email function host
    /// </summary>
    public const string NOTIFICATION_EMAIL_FUNCTION_HOST = "NotificationEmailFunction";

    /// <summary>
    /// The organization daas host
    /// </summary>
    public const string ORGANIZATION_DAAS_HOST = "OrganizationDaas";

    /// <summary>
    /// The patient daas host
    /// </summary>
    public const string PATIENT_DAAS_HOST = "PatientDaas";

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