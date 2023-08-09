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
    public const string EFORM_HOST = "EFormService";

    /// <summary>
    /// The acm host
    /// </summary>
    public const string ACM_HOST = "AcmService";

    /// <summary>
    /// The alert host
    /// </summary>
    public const string ALERT_HOST = "AlertService";

    /// <summary>
    /// The MDM host
    /// </summary>
    public const string MDM_HOST = "MdmService";

    /// <summary>
    /// The notification host
    /// </summary>
    public const string NOTIFICATION_HOST = "NotificationService";

    /// <summary>
    /// The utility file
    /// </summary>
    public const string UTILITY_FILE = "UtilityFile";

    /// <summary>
    /// The utility notification inbox host
    /// </summary>
    public const string UTILITY_NOTIFICATION_INBOX_HOST = "UtilityNotificationInbox";

    /// <summary>
    /// The utility notification SMS host
    /// </summary>
    public const string UTILITY_NOTIFICATION_SMS_HOST = "UtilityNotificationSms";

    /// <summary>
    /// The utility notification email host
    /// </summary>
    public const string UTILITY_NOTIFICATION_EMAIL_HOST = "UtilityNotificationEmail";

    /// <summary>
    /// The organization daas host
    /// </summary>
    public const string ORGANIZATION_DAAS_HOST = "OrganizationDaas";

    /// <summary>
    /// The patient daas host
    /// </summary>
    public const string PATIENT_DAAS_HOST = "PatientDaas";

    /// <summary>
    /// The doctor daas host
    /// </summary>
    public const string DOCTOR_DAAS_HOST = "DoctorDaas";

    /// <summary>
    /// The price daas host
    /// </summary>
    public const string PRICE_DAAS_HOST = "PriceDaas";

    /// <summary>
    /// The action log export count
    /// </summary>
    public const string ACTION_LOG_EXPORT_COUNT = "ActionLogExportCount";

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