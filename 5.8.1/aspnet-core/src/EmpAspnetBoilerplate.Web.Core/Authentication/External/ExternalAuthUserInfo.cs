namespace EmpAspnetBoilerplate.Authentication.External
{
    public class ExternalAuthUserInfo
    {
        public string ProviderKey { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Surname { get; set; }

        public string Provider { get; set; }

        public string Address { get; set; }
        public string Qualification { get; set; }
        public string ContactNumber { get; set; }
        public int DepartmentId { get; set; }

    }
}
