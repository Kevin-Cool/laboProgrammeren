using Microsoft.EntityFrameworkCore;
using programeren_3_eindwerk.classes;
using programeren_3_eindwerk.Deel2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace programeren_3_eindwerk.Deel3
{
    class DomainController
    {
        private readonly ProvinciesContext _context;
        private readonly DbSet<Provincie> _provincies;
        private readonly DbSet<Straat> _straaten;
        private readonly DbSet<Gemeente> _gemeenten;
        public DomainController(ProvinciesContext provinciesContext)
        {
            _context = provinciesContext;
            _provincies = provinciesContext.Provincies;
            _straaten = provinciesContext.Straten;
            _gemeenten = provinciesContext.Gemeenten;

        }

        public List<int> GeefLijstStraatIDsVanGemeente(int gemeenteID)
        {
            List<int> returnWaardes = new List<int>();
            foreach (Provincie provincie in _provincies.Include(x => x.Gemeenten).ThenInclude(y => y.Straten))
            {
                if (provincie.Gemeenten.Any(x => x.GemeenteID == gemeenteID))
                {
                    List<Straat> tempStraten =  provincie.Gemeenten.First(x => x.GemeenteID == gemeenteID).Straten;
                    foreach (Straat tempStraatID in tempStraten)
                    {
                        returnWaardes.Add(tempStraatID.StraatID);
                    }
                    return returnWaardes;

                }
            }
            return returnWaardes;
        }
        public Straat GeefStraatOpBasisVanID(int straatID)
        {
            return _straaten.FirstOrDefault(x => x.StraatID == straatID);
        }
        public Straat GeefStraatOpBasisVanNaam(string straatNaam)
        {
            return _straaten.FirstOrDefault(x => x.Straatnaam == straatNaam);
        }
        public List<string> GeefLijstVanStraatNamenVanGemeente(int gemeenteID)
        {
            List<Straat> tempStraat =  _gemeenten.Include(x => x.Straten).First(y => y.GemeenteID == gemeenteID).Straten;
            tempStraat = tempStraat.OrderBy(x => x.Straatnaam).ToList();
            List<string> antwoordString = new List<string>();
            foreach (Straat ts in tempStraat)
            {
                antwoordString.Add(ts.Straatnaam);
            }
            return antwoordString;
        }
        public string GeefRaportVanProvincie(int provincieID)
        {
            List<Gemeente> TempGemeentes = _provincies.Include(x => x.Gemeenten).ThenInclude(y => y.Straten).ThenInclude(z => z.Graaf).ThenInclude(q => q.ListMap).First(x => x.ProvincieID == provincieID).Gemeenten;
            string BuilderString = "";
            foreach (Gemeente gemeente in TempGemeentes)
            {
                BuilderString += $"{gemeente.GemeenteNaam}: {gemeente.Straten.Count}\n";
                foreach (Straat straat in gemeente.Straten)
                {
                    BuilderString += $"  *   {straat.Straatnaam.TrimEnd()},{straat.Graaf.Map.Count}\n";
                }
            }
            return BuilderString;
        }
    }
}
