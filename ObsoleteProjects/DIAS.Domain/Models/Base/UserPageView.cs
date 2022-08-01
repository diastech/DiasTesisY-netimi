namespace DIAS.Domain.Models.Base
{
    //User Page Permissions view
    public partial class UserPageView
    {
        public string UserId { get; set; }
        public string PageId { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public bool Expanded { get; set; }
        public string Image { get; set; }
    }
    /*
		CREATE OR REPLACE VIEW diasproject.`UserPageView` AS
        SELECT 
                up.applicationUserId AS 'UserId', 
                up.applicationPageId AS 'PageId',
                p.ParentId,
                p.Level AS 'Level',
                p.Order AS 'Order',
                p.Text as 'Text', 
                p.Path as 'Path', 
                p.Icon as 'Icon', 
                p.Expanded, 
                p.Image   
        FROM 
            diasproject.userpage up, 
            diasproject.page p 
        WHERE 
            up.applicationPageId = p.id
            AND p.isActive = true


        CREATE VIEW UserPageView AS
        SELECT 
                up.applicationUserId AS 'UserId', 
                up.applicationPageId AS 'PageId',
                p.ParentId,
                p.Level AS 'Level',
                p.[Order] AS 'Order',
                p.Text as 'Text', 
                p.Path as 'Path', 
                p.[icon] as 'Icon', 
                p.Expanded, 
                p.[image]   
        FROM 
            userpage up, 
            [page] p 
        WHERE 
            up.applicationPageId = p.id
            AND p.isActive = 1
    */
}
