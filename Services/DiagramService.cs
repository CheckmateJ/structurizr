using Structurizr;
using System.IO;
using Newtonsoft.Json;

namespace structurizr.Services
{
    public class DiagramService
    {
        public string GenerateDiagram(string data)
        {
                        Workspace workspace = new Workspace("Getting Started", "This is a model of my software system.");
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

            // Serializacja do JSON
            string json = JsonConvert.SerializeObject(workspace, Formatting.Indented);

            // Zapis do pliku
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "diagram.json");
            File.WriteAllText(filePath, json);

            return "/diagram.json";
        }
    }
}
