using Structurizr;
using System.IO;
using Newtonsoft.Json;
using Structurizr.Api;

namespace structurizr.Services
{
    public class DiagramService
    {
        private readonly string _structurizrKey;
        private readonly string _structurizrSecret;
        private readonly string _structurizrId;
        private long _id;

        public DiagramService()
        {
            _structurizrKey = Environment.GetEnvironmentVariable("STRUCTURIZR_KEY");
            _structurizrSecret = Environment.GetEnvironmentVariable("STRUCTURIZR_SECRET");
            _structurizrId = Environment.GetEnvironmentVariable("STRUCTURIZR_ID");
        }
        public string GenerateDiagram(string data)
        {
            Workspace workspace = new Workspace("New start", "This is a model of my software system.");
            Model model = workspace.Model;

            Person user = model.AddPerson("User", "A user of my software system.");
            SoftwareSystem softwareSystem = model.AddSoftwareSystem("Software System", "My software system.");
            user.Uses(softwareSystem, "Uses");

            ViewSet viewSet = workspace.Views;
            SystemContextView contextView = viewSet.CreateSystemContextView(softwareSystem, "SystemContext", "An example of a System Context diagram.");
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.SoftwareSystem) { Background = "#1168bd", Color = "#ffffff" });
            styles.Add(new ElementStyle(Tags.Person) { Background = "#08427b", Color = "#ffffff", Shape = Shape.Person });

            string json = JsonConvert.SerializeObject(workspace, Formatting.Indented);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "diagram.json");
            File.WriteAllText(filePath, json);

            StructurizrClient structurizrClient = new StructurizrClient(_structurizrKey, _structurizrSecret);


            long.TryParse(_structurizrId, out _id);
            structurizrClient.PutWorkspace(_id, workspace);

            return "/diagram.json";
        }
    }
}
