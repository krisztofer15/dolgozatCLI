using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//A GitHub linket kell beadni!!! -> IKT jegyként is be lesz írva

// FELADAT:
//0.Alapértelmezett parancssoros és grafikus projekt létrehozása.
//UtazasokGUI
//UtazasokCLI
//1. Adattároló osztály és kapcsolódó repository létrehozása
//Utazas (ToString, static CreateFromCSVLine)
//UtazasRepository(FindAll)
//2.Adatok tesztelése az UtazasCLI programban (Console.WriteLine)
//3. Adatok tesztelése az UtazasGUI-ban (DataGridView)
//(szerkesztés letiltása, Path beállítása a repository-ban és adatok betöltése az OnLoad esemény lefutásakor)
//-----------------------
//EDDIG ELÉGSÉGES
//-----------------------
//4. Menü megvalósítása
//Fájl 
//- Megnyitás
//- Kilépés
//Súgü
//- Névjegy (rövid üzenet megjelenítése)
//(FileOpenDialog: FileName, Filter, Name)
//-----------------------
//EDDIG KÖZEPES (ha tényleg a kiválasztott fájl töltődik be)
//----------------------- 
//5. string ToCSVLine() hozzáadása az Utazas osztályhoz
//6. FindById(int id) Save(Person person) metódus hozzáadása az UtazasRepository-hoz
//Save: ha utazas.Id == 0(vagy null), akkor új elem létrehozása (maxId + 1), egyébként meglévő frissítése
//-----------------------
//EDDIG JÓ  (ha a Save működi a parancssoros verzióban)
//-----------------------  
//7. Utazas, UtazasRepository osztályok átmásolása az UtazasGUI projektbe
//8. Új elem létrehozása
//Fájl / Új / Utazás menüpont létrehozása
//Második ablak létrehozása és megjelenítése a ShowDialog-al (ezt néztük közösen) vagy űrlap a táblázat mellett a SplitContainer segítségével (ezt a keresésnél néztük).
//-----------------------
//EDDIG JELES  (ha a grafikus felületen is lehet új )

//CSV PELDA:
//id, orszag, honap, nap, hossz, ar, ellatas
//1, Spanyolország,7,1,14,420000, reggeli
//2, Törökország,7,21,13,250000, félpanzió
//3, Olaszország,7,15,13,400000, félpanzió
//4, Olaszország,7,9,11,190000, félpanzió


namespace UtazasokCLI
{

    public class Utazas
    {
        public int Id { get; set; }
        public string Orszag { get; set; }
        public int Honap { get; set; }
        public int Nap { get; set; }
        public int Hossz { get; set; }
        public int Ar { get; set; }
        public string Ellatas { get; set; }

        public Utazas(int id, string orszag, int honap, int nap, int hossz, int ar, string ellatas)
        {
            Id = id;
            Orszag = orszag;
            Honap = honap;
            Nap = nap;
            Hossz = hossz;
            Ar = ar;
            Ellatas = ellatas;
        }

        public override string ToString()
        {
            return $"Utazás {Id}: {Orszag}, {Honap}/{Nap}, Hossz: {Hossz} nap, Ár: {Ar}, Ellátás: {Ellatas}";
        }

        public static Utazas CreateFromCSVLine(string csvLine)
        {
            string[] parts = csvLine.Split(',');
            int id = int.Parse(parts[0]);
            string orszag = parts[1];
            int honap = int.Parse(parts[2]);
            int nap = int.Parse(parts[3]);
            int hossz = int.Parse(parts[4]);
            int ar = int.Parse(parts[5]);
            string ellatas = parts[6];

            return new Utazas(id, orszag, honap, nap, hossz, ar, ellatas);
        }

        public string ToCSVLine()
        {
            return $"{Id},{Orszag},{Honap},{Nap},{Hossz},{Ar},{Ellatas}";
        }
    }

    public class UtazasRepository
    {
        private List<Utazas> utazasok;

        public UtazasRepository()
        {
            utazasok = new List<Utazas>();
        }

        public List<Utazas> FindAll()
        {
            return utazasok;
        }

        public Utazas FindById(int id)
        {
            return utazasok.Find(u => u.Id == id);
        }

        public void Save(Utazas utazas)
        {
            if (utazas.Id == 0)
            {
                int maxId = utazasok.Count > 0 ? utazasok.Max(u => u.Id) : 0;
                utazas.Id = maxId + 1;
                utazasok.Add(utazas);
            }
            else
            {
                int index = utazasok.FindIndex(u => u.Id == utazas.Id);
                if (index != -1)
                {
                    utazasok[index] = utazas;
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            UtazasRepository repo = new UtazasRepository();

            string[] lines = File.ReadAllLines("./utazasok.csv");

            Console.WriteLine("Utazasok:");
            for (int i = 1; i < lines.Length; i++)
            {
                Utazas utazas = Utazas.CreateFromCSVLine(lines[i]);
                repo.Save(utazas);

                Console.WriteLine(lines[i]);
            }

            Console.ReadLine();
        }
    }
}
