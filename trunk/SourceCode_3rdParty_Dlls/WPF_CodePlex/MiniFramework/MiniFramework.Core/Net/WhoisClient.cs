// <copyright file="WhoisClient.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Net.WhoisClient</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 20:13:52 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1984 $</version>

namespace MiniFramework.Net
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Sockets;
	using System.Text;
	using System.Text.RegularExpressions;

	using MiniFramework.Properties;
	
	//// ========================================================================================

	/// <summary>
	/// Fournit des connexions client pour des services de recherche Whois.
	/// </summary>
	public class WhoisClient: IDisposable
	{
		/// <summary>
		/// Numéro de port utilisé par défaut.
		/// </summary>
		private const int DefaultPort=43;
		
		/// <summary>
		/// Délai d'expiration utilisé par défaut.
		/// </summary>
		private const int DefaultTimeout=30*1000;
		
		/// <summary>
		/// Dictionnaire de correspondance entre les domaines de haut niveau et les services de recherche Whois connus.
		/// </summary>
		private static readonly SortedDictionary<string, string> knownServers=new SortedDictionary<string, string>
		{
			{ "ac", "whois.nic.ac" },
			{ "ae", "whois.uaenic.ae" },
			{ "aero", "whois.aero" },
			{ "af", "whois.nic.af" },
			{ "ag", "whois.nic.ag" },
			{ "ai", "whois.offshore.ai" },
			{ "al", "whois.ripe.net" },
			{ "am", "whois.amnic.net" },
			{ "ar", "www.nic.ar" },
			{ "as", "whois.nic.as" },
			{ "at", "whois.nic.at" },
			{ "au", "whois.aunic.net" },
			{ "az", "whois.ripe.net" },
			{ "ba", "whois.ripe.net" },
			{ "be", "whois.dns.be" },
			{ "bg", "whois.register.bg" },
			{ "bi", "www.nic.bi" },
			{ "biz", "whois.neulevel.biz" },
			{ "bj", "whois.nic.bj" },
			{ "bm", "rwhois.ibl.bm" },
			{ "br", "whois.nic.br" },
			{ "bv", "whois.ripe.net" },
			{ "by", "whois.ripe.net" },
			{ "bz", "whois.belizenic.bz" },
			{ "ca", "whois.cira.ca" },
			{ "cat", "whois.cat" },
			{ "cc", "whois.nic.cc" },
			{ "cd", "whois.nic.cd" },
			{ "cg", "www.nic.cg" },
			{ "ch", "whois.nic.ch" },
			{ "ci", "whois.nic.ci" },
			{ "ck", "whois.nic.ck" },
			{ "cl", "whois.nic.cl" },
			{ "cn", "whois.cnnic.net.cn" },
			{ "com", "whois.verisign-grs.com" },
			{ "coop", "whois.nic.coop" },
			{ "cx", "whois.nic.cx" },
			{ "cy", "whois.ripe.net" },
			{ "cz", "whois.nic.cz" },
			{ "de", "whois.denic.de" },
			{ "dk", "whois.dk-hostmaster.dk" },
			{ "dm", "whois.nic.dm" },
			{ "do", "whois.nic.do" },
			{ "dz", "whois.ripe.net" },
			{ "ec", "www.nic.ec" },
			{ "edu", "whois.educause.net" },
			{ "ee", "whois.eenet.ee" },
			{ "eg", "whois.ripe.net" },
			{ "es", "www.nic.es" },
			{ "eu", "whois.eu" },
			{ "fi", "whois.ficora.fi" },
			{ "fj", "whois.usp.ac.fj" },
			{ "fm", "www.dot.fm" },
			{ "fo", "whois.ripe.net" },
			{ "fr", "whois.nic.fr" },
			{ "gg", "whois.isles.net" },
			{ "gi", "www.nic.gi" },
			{ "gov", "whois.nic.gov" },
			{ "gm", "whois.ripe.net" },
			{ "gp", "whois.nic.gp" },
			{ "gr", "whois.ripe.net" },
			{ "gs", "203.119.12.22" },
			{ "gt", "www.gt" },
			{ "hk", "whois.hkdnr.net.hk" },
			{ "hm", "whois.registry.hm" },
			{ "hr", "www.dns.hr" },
			{ "hu", "whois.nic.hu" },
			{ "id", "whois.idnic.net.id" },
			{ "ie", "whois.domainregistry.ie" },
			{ "il", "whois.isoc.org.il" },
			{ "im", "whois.nic.im" },
			{ "in", "whois.inregistry.in" },
			{ "info", "whois.afilias.info" },
			{ "int", "whois.iana.org" },
			{ "io", "www.io.io" },
			{ "ir", "whois.nic.ir" },
			{ "is", "whois.isnic.is" },
			{ "it", "whois.nic.it" },
			{ "je", "whois.isles.net" },
			{ "jp", "whois.jprs.jp" },
			{ "kg", "whois.domain.kg" },
			{ "ki", "whois.nic.ki" },
			{ "kr", "whois.krnic.net" },
			{ "kz", "whois.nic.kz" },
			{ "la", "whois.nic.la" },
			{ "lb", "cgi.aub.edu.lb" },
			{ "li", "whois.nic.li" },
			{ "lk", "whois.nic.lk" },
			{ "lt", "whois.domreg.lt" },
			{ "lu", "whois.dns.lu" },
			{ "lv", "whois.nic.lv" },
			{ "ly", "whois.nic.ly" },
			{ "ma", "whois.ripe.net" },
			{ "mil", "whois.nic.mil" },
			{ "mk", "whois.ripe.net" },
			{ "mm", "whois.nic.mm" },
			{ "mobi", "whois.dotmobiregistry.net" },
			{ "ms", "whois.adamsnames.tc" },
			{ "mt", "www.um.edu.mt" },
			{ "mu", "whois.nic.mu" },
			{ "museum", "whois.museum" },
			{ "mw", "www.tarsus.net" },
			{ "mx", "whois.nic.mx" },
			{ "my", "whois.mynic.net.my" },
			{ "na", "whois.na-nic.com.na" },
			{ "name", "whois.nic.name" },
			{ "net", "whois.verisign-grs.com" },
			{ "ng", "whois.rg.net" },
			{ "nl", "whois.domain-registry.nl" },
			{ "no", "whois.norid.no" },
			{ "nu", "whois.nic.nu" },
			{ "nz", "whois.srs.net.nz" },
			{ "org", "whois.publicinterestregistry.net" },
			{ "pe", "whois.nic.pe" },
			{ "pk", "pknic.net.pk" },
			{ "pl", "whois.dns.pl" },
			{ "pm", "whois.nic.pm" },
			{ "pro", "whois.registrypro.pro" },
			{ "pt", "whois.dns.pt" },
			{ "pw", "whois.nic.pw" },
			{ "re", "whois.nic.re" },
			{ "ro", "whois.rotld.ro" },
			{ "ru", "whois.ripn.net" },
			{ "rw", "www.nic.rw" },
			{ "sa", "saudinic.net.sa" },
			{ "se", "whois.iis.se" },
			{ "sg", "whois.nic.net.sg" },
			{ "sh", "whois.nic.sh" },
			{ "si", "whois.arnes.si" },
			{ "sj", "whois.ripe.net" },
			{ "sk", "whois.ripe.net" },
			{ "sm", "whois.ripe.net" },
			{ "sr", "whois.register.sr" },
			{ "st", "whois.nic.st" },
			{ "su", "whois.ripn.net" },
			{ "tc", "whois.adamsnames.tc" },
			{ "tf", "whois.afnic.fr" },
			{ "tg", "www.nic.tg" },
			{ "th", "whois.thnic.net" },
			{ "tj", "whois.nic.tj" },
			{ "tk", "whois.dot.tk" },
			{ "tl", "whois.nic.tl" },
			{ "tm", "whois.nic.tm" },
			{ "tn", "whois.ripe.net" },
			{ "to", "whois.tonic.to" },
			{ "tr", "whois.nic.tr" },
			{ "tt", "www.nic.tt" },
			{ "tv", "whois.nic.tv" },
			{ "tw", "whois.twnic.net" },
			{ "ua", "whois.com.ua" },
			{ "ug", "whois.co.ug" },
			{ "uk", "whois.nic.uk" },
			{ "us", "whois.nic.us" },
			{ "uy", "www.rau.edu.uy" },
			{ "uz", "www.noc.uz" },
			{ "va", "whois.ripe.net" },
			{ "ve", "whois.nic.ve" },
			{ "vg", "whois.adamsnames.tc" },
			{ "vi", "www.nic.vi" },
			{ "vn", "www.vnnic.net.vn" },
			{ "vu", "www.vunic.vu" },
			{ "wf", "whois.nic.wf" },
			{ "ws", "whois.worldsite.ws" },
			{ "yt", "whois.nic.yt" },
			{ "yu", "whois.ripe.net" },
			{ "za", "whois.frd.ac.za" }
		};

		/// <summary>
		/// Valeur indiquant si les ressources du client ont été libérées.
		/// </summary>
		private bool disposed;

		/// <summary>
		/// Client TCP utilisé pour interroger les serveurs Whois.
		/// </summary>
		private TcpClient tcpClient;

		//// =====================================================================================
		
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="WhoisClient" />.
		/// </summary>
		public WhoisClient(): this(null) {}
		
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="WhoisClient" /> et établit une connexion sur l'hôte spécifié fournissant un service de recherche Whois.
		/// </summary>
		/// <param name="hostName">Nom DNS de l'hôte distant.</param>
		public WhoisClient(string hostName)
		{
			this.tcpClient=new TcpClient();

			this.Port=DefaultPort;
			this.Timeout=DefaultTimeout;
			
			if(!string.IsNullOrEmpty(hostName)) this.Connect(hostName);
		}

		/// <summary>
		/// Autorise l'objet en cours à tenter de libérer des ressources et d'exécuter d'autres opérations de nettoyage avant qu'il ne soit récupéré par l'opération de ramasse-miettes.
		/// </summary>
		/// <remarks>
		/// Les types dérivant de cette classe ne devraient pas fournir de destructeur.
		/// </remarks>
		~WhoisClient()
		{
			this.Dispose(false);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient une valeur indiquant si le client est connecté à un hôte distant. 
		/// </summary>
		/// <value><see langword="true" /> si le client était connecté à une ressource distante lors de l'opération la plus récente ; sinon, <see langword="false" />.</value>
		public bool Connected
		{
			get { return this.tcpClient.Connected; }
		}

		/// <summary>
		/// Obtient ou définit le numéro de port utilisé pour se connecter à un hôte distant. 
		/// </summary>
		/// <value>Numéro de port utilisé pour se connecter à un hôte distant. La valeur par défaut est 43.</value>
		public int Port
		{
			get;
			set;
		}

		/// <summary>
		/// Obtient ou définit le délai d'expiration des demandes en millisecondes.
		/// </summary>
		/// <value>Valeur du délai d'expiration des demandes en millisecondes. La valeur par défaut est 30 secondes.</value>
		public int Timeout
		{
			get { return this.tcpClient.ReceiveTimeout; }
			
			set
			{
				this.tcpClient.ReceiveTimeout=value;
				this.tcpClient.SendTimeout=value;
			}
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient un dictionnaire de correspondance entre les domaines de haut niveau et les services de recherche Whois connus.
		/// </summary>
		/// <value>Dictionnaire dont les clés sont les domaines de haut niveau et les valeurs sont les noms DNS des hôtes distants fournissant un service de recherche Whois approprié.</value>
		public static IDictionary<string, string> KnownServers
		{
			get { return knownServers; }
		}

		//// =====================================================================================

		/// <summary>
		/// Supprime cette instance et ferme la connexion sous-jacente.
		/// </summary>
		public void Close()
		{
			((IDisposable) this).Dispose();
		}

		/// <summary>
		/// Connecte le client sur l'hôte spécifié fournissant un service de recherche Whois.
		/// </summary>
		/// <param name="hostName">Nom DNS de l'hôte distant.</param>
		public void Connect(string hostName)
		{
			this.tcpClient.Connect(hostName, this.Port);
		}

		/// <summary>
		/// Exécute les tâches associées à la libération ou à la redéfinition des ressources.
		/// </summary>
		/// <remarks>
		/// Cette méthode prend en charge l'infrastructure de la bibliothèque de classes et n'est pas destinée à être utilisée directement à partir de votre code.
		/// </remarks>
		/// <seealso cref="Close" />
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Interroge le service Whois de l'hôte distant et retourne les informations du nom de domaine spécifié.
		/// </summary>
		/// <remarks>
		/// L'appelant doit qu'une connexion a préalablement été ouverte avec la méthode <see cref="Connect" />.
		/// </remarks>
		/// <param name="domain">Nom de domaine à rechercher.</param>
		/// <returns>Résultat de l'interrogation du service de recherche Whois sur l'hôte distant.</returns>
		/// <exception cref="ArgumentException">Le nom de domaine spécifié est une chaîne vide ou une référence null.</exception>
		public string Lookup(string domain)
		{
			if(string.IsNullOrEmpty(domain)) throw new ArgumentException(Resources.EmptyDomainName, "domain");
			
			// Envoi de la requête Whois
			var query=Encoding.ASCII.GetBytes(domain+"\r\n");
			using(var stream=this.tcpClient.GetStream())
			{
				stream.Write(query, 0, query.Length);
				
				// Lecture de la réponse
				using(var reader=new StreamReader(stream, Encoding.ASCII))
					return Regex.Replace(reader.ReadToEnd().Trim(), @"\r?\n", Environment.NewLine);
			}
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient des informations sur le nom de domaine de deuxième niveau spécifié.
		/// </summary>
		/// <remarks>
		/// Cette méthode sélectionne automatiquement le service de recherche Whois approprié en fonction du domaine de haut niveau présent dans le nom de domaine spécifié.
		/// </remarks>
		/// <param name="domain">Nom de domaine de deuxième niveau à rechercher.</param>
		/// <returns>Résultat de l'interrogation du service de recherche Whois sur l'hôte distant.</returns>
		/// <exception cref="ArgumentException">Le nom de domaine spécifié est invalide ou une référence null.</exception>
		public static string GetHostInfo(string domain)
		{
			if(string.IsNullOrEmpty(domain)) throw new ArgumentException(Resources.EmptyDomainName, "domain");

			// Détermination du domaine de haut niveau
			var index=domain.LastIndexOf('.');
			if(index<=0) throw new ArgumentException(Resources.InvalidDomainName, "domain");
			
			var tld=domain.Substring(index+1);
			if(!knownServers.ContainsKey(tld)) throw new ArgumentException(Resources.UnknownTopLevelDomain, "domain");

			// Interrogation du serveur Whois
			using(var client=new WhoisClient(knownServers[tld])) return client.Lookup(domain);
		}

		//// =====================================================================================

		/// <summary>
		/// Exécute les tâches associées à la libération ou à la redéfinition des ressources.
		/// </summary>
		/// <param name="disposing">Valeur indiquant si les ressources gérées doivent être libérées.</param>
		protected virtual void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing) this.tcpClient.Close();
				this.disposed=true;
			}
		}
	}
}