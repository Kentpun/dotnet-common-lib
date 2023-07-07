namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Department Tree Response
    /// </summary>
    /// <seealso cref="DepartmentResponse" />
    public class DepartmentTreeResponse : DepartmentResponse
    {
        /// <summary>
        /// Gets or sets the sub departments.
        /// </summary>
        /// <value>
        /// The sub departments.
        /// </value>
        public List<DepartmentTreeResponse>? SubDepartments { get; set; }
    }
}