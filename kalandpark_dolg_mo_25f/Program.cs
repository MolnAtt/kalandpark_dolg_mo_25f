using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalandpark_dolg_mo_25f
{
	internal class Program
	{
		class Fa
		{
			public int id;
			public int magassag;
			public string tipus;

			public Fa(string[] t)
			{
				this.id = int.Parse(t[0]);
				this.magassag = int.Parse(t[1]);
				this.tipus = t[2];
			}
		}

		static List<Fa> Beolvas(string fajlnev)
		{
			List<Fa> fak = new List<Fa>();
			StreamReader sr = new StreamReader(fajlnev, Encoding.Default);
			while (!sr.EndOfStream)
			{
				fak.Add(new Fa(sr.ReadLine().Split(';')));
			}
			sr.Close();
			return fak;
		}
		static int Megszamlalas_1(List<Fa> fak, string tipus)
		{
			int db = 0;
			for (int i = 0; i < fak.Count; i++)
			{
				if (fak[i].tipus == tipus)
				{
					db++;
				}
			}
			return db;
		}
		static int Maximumszamitas_2(List<Fa> fak)
		{
			int max = fak[0].magassag;
			for (int i = 1; i < fak.Count; i++)
			{
				if (fak[i].magassag > max)
				{
					max = fak[i].magassag;
				}
			}
			return max;
		}

		static List<Fa> Kivalogatas_3(List<Fa> fak, int magassag, string tipus)
		{
			List<Fa> result = new List<Fa>();

			for (int i = 0; i < fak.Count; i++)
			{
				if (fak[i].magassag > magassag && fak[i].tipus == tipus)
				{
					result.Add(fak[i]);
				}
			}
			return result;
		}

		static bool Eldontes_4(List<Fa> fak, string tipus)
		{
			int i = 0;
			while (i < fak.Count && fak[i].tipus != tipus)
			{
				i++;
			}
			return i < fak.Count;
		}

		static Fa Kereses_5(List<Fa> fak, int magassag, string tipus)
		{
			int i = 0;
			while (i < fak.Count && !(fak[i].magassag<magassag && fak[i].tipus == tipus))
			{
				i++;
			}
			if (i < fak.Count) // de nem baj, ha nincs hibakezelve!
			{
				return fak[i];
			}
			else 
			{
                Console.WriteLine("Nincs is ilyen fa!");
				return null;
			}
		}

		static Dictionary<string, int> Csoportositas_maximummal_6(List<Fa> fak)
		{
			Dictionary<string, int> szotar = new Dictionary<string, int>();

			foreach (Fa fa in fak)
			{
				if (szotar.ContainsKey(fa.tipus))
				{
					if (szotar[fa.tipus] < fa.magassag)
					{
						szotar[fa.tipus] = fa.magassag;
					}
				}
				else
				{
					szotar[fa.tipus] = fa.magassag;
				}
			}

			return szotar;
		}


		static void Main(string[] args)
		{
			List<Fa> fak = Beolvas("4000A_fak.tsv");

			int f1 = Megszamlalas_1(fak, "vadgesztenye");
			Console.WriteLine($"1. vadgesztenyék száma a kalandparkban: {f1}");

			int f2 = Maximumszamitas_2(fak);
			Console.WriteLine($"2. a legmagasabb fa: {f2}");

			List<Fa> f3 = Kivalogatas_3(fak, 2000, "tolgy");
			Console.WriteLine($"3. a 20 méternél magasabb tölgyek: ");

			foreach (Fa fa in f3)
			{
                Console.WriteLine($"id: {fa.id}, {fa.tipus} ({fa.magassag} cm)");
			}

			bool f4 = Eldontes_4(fak, "koris");
			if (f4)
			{
				Console.WriteLine($"4. van a kalandparkban kőris");
			}
			else
			{
				Console.WriteLine($"4. nincs a kalandparkban kőris");
			}

			Fa f5 = Kereses_5(fak, 2000, "eger");

            Console.WriteLine($"Ez az első 20m-nél alacsonyabb éger: {f5.id}, {f5.tipus} ({f5.magassag} cm)");

			Dictionary<string, int> f6 = Csoportositas_maximummal_6(fak);

			foreach (string kulcs in f6.Keys)
			{
                Console.WriteLine($"{kulcs}: {f6[kulcs]}");
			}
		}

	}
}
