namespace ToDoList.Web.Api.Configuration
{
    /// <summary>
    /// Swagger option model 
    /// </summary>
    public class SwaggerSettings
    {
        // Api name
        public string Name { get; set; }
        // Api title
        public string Title { get; set; }
        // Api description
        public string Description { get; set; }
        // Api version
        public string Version { get; set; }

        public string EndPoint { get; set; }
    }
}
