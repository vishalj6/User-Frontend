namespace UsersProject.Models
{
    public class PaginationUser
    {
        public List<UserPerson> AllUsers { get; set; }
        //public int CurrentPageIndex { get; set; }
        public int total_pages { get; set; } = 1;
        public int? total_users { get; set; }


        //public string Search { get; set; } = "";
        //public int LimitDropDown { get; set; }
    }
}
