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
    /// The eform host
    /// </summary>
    public const string EFORM_HOST = "EForm";

    /// <summary>
    /// The acm host
    /// </summary>
    public const string ACM_HOST = "Acm";

    /// <summary>
    /// The alert host
    /// </summary>
    public const string ALERT_HOST = "Alert";

    /// <summary>
    /// The MDM host
    /// </summary>
    public const string MDM_HOST = "Mdm";

    /// <summary>
    /// The notification host
    /// </summary>
    public const string NOTIFICATION_HOST = "Notification";

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
    /// The file function
    /// </summary>
    public const string FILE_FUNCTION = "fileFunction";

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