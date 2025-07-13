using Microsoft.AspNetCore.Mvc.Rendering;
using P16OWWiki2.Models;

namespace P16OWWiki2.ViewModels
{
    public class BuscaVM
    {
        public List<Heroe> LosHeroes { get; set; }
        public SelectList LosRoles { get; set; }
        public SelectList LasNacionalidades { get; set; }
        public SelectList LasAfiliaciones { get; set; }

        public string BNom { get; set; }
        public string BRol { get; set; }
        public string BNac { get; set; }
        public string BAfi { get; set; }
    }
}
