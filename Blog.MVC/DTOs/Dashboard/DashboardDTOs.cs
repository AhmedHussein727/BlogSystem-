namespace Blog.MVC.DTOs.Dashboard
{
    public class DashboardDTO
    {
        public int TotalPosts { get; set; }

        public int TotalComments { get; set; }

        public int TotalCategories { get; set; }

        public int TotalUsers { get; set; }

        public List<string> RecentPosts { get; set; }
            = new();

        public List<string> RecentCategories { get; set; }
            = new();
    }
}
