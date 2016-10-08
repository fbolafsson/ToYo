using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using ToYo.Web.Models;

namespace ToYo.Web.Services
{
    public class AgentRepository
    {
        private IList<Agent> agents;

        public AgentRepository()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ToYo.Web.Agents.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                agents = JsonConvert.DeserializeObject<List<Agent>>(result);
            }
        }

        public IList<Agent> GetAgents()
        {
            return agents;
        }

        public Agent GetAgent(int id)
        {
            return agents.Single(x => x.Id == id);
        }
    }
}