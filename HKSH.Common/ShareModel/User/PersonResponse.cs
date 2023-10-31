namespace HKSH.Common.ShareModel.User
{
    public class PersonResponse
    {
        public int? PersonId { get; set; }

        public int? PersonnelId { get; set; }

        public string? ChineseName {  get; set; }

        public string? EnglishName { get; set; }

        public string? PersonnelType { get; set; }

        public bool? IsHospitalRMO { get; set;}

        public int? JobTitleId { get; set; }

        public string? JobTitleDescriptionEn { get; set; }

        public string? JobTitleDescriptionZh { get; set; }
    }
}
