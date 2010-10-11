// <copyright file="StockIcons.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Resources.StockIcons</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 11:16:06 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2020 $</version>

namespace MiniFramework.Resources
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Resources;

	using MiniFramework.Drawing;

	//// ========================================================================================

	/// <summary>
	/// Fournit une collection d'objets <see cref="StockIcon" /> représentant des icônes standard.
	/// </summary>
	public static class StockIcons
	{
		/// <summary>
		/// Instances des icônes mises en cache.
		/// </summary>
		private static Dictionary<string, StockIcon> icons=new Dictionary<string, StockIcon>();

		/// <summary>
		/// Gestionnaire utilisé pour accéder aux ressources de la classe.
		/// </summary>
		private static ResourceManager resources=new ResourceManager(typeof(StockIcons));

		//// =====================================================================================
		
		/// <summary>
		/// Obtient l'icône standard &quot;A propos&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;A propos&quot;.</value>
		public static StockIcon About
		{
			get { return GetStockIcon("About"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Accepter&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Accepter&quot;.</value>
		public static StockIcon Accept
		{
			get { return GetStockIcon("Accept"); }
		}
			
		/// <summary>
		/// Obtient l'icône standard &quot;Appliquer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Appliquer&quot;.</value>
		public static StockIcon Apply
		{
			get { return GetStockIcon("Apply"); }
		}
			
		/// <summary>
		/// Obtient l'icône standard &quot;Bogue&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Bogue&quot;.</value>
		public static StockIcon Bug
		{
			get { return GetStockIcon("Bug"); }
		}
			
		/// <summary>
		/// Obtient l'icône standard &quot;Construire&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Construire&quot;.</value>
		public static StockIcon Build
		{
			get { return GetStockIcon("Build"); }
		}
	
		/// <summary>
		/// Obtient l'icône standard &quot;Annuler&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Annuler&quot;.</value>
		public static StockIcon Cancel
		{
			get { return GetStockIcon("Cancel"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Effacer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Effacer&quot;.</value>
		public static StockIcon Clear
		{
			get { return GetStockIcon("Clear"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Fermer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Fermer&quot;.</value>
		public static StockIcon Close
		{
			get { return GetStockIcon("Close"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Copier&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Copier&quot;.</value>
		public static StockIcon Copy
		{
			get { return GetStockIcon("Copy"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Crédits&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Crédits&quot;.</value>
		public static StockIcon Credits
		{
			get { return GetStockIcon("Credits"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Couper&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Couper&quot;.</value>
		public static StockIcon Cut
		{
			get { return GetStockIcon("Cut"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Base de données&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Base de données&quot;.</value>
		public static StockIcon Database
		{
			get { return GetStockIcon("Database"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Supprimer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Supprimer&quot;.</value>
		public static StockIcon Delete
		{
			get { return GetStockIcon("Delete"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Erreur&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Erreur&quot;.</value>
		public static StockIcon Error
		{
			get { return GetStockIcon("Error"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Quitter&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Quitter&quot;.</value>
		public static StockIcon Exit
		{
			get { return GetStockIcon("Exit"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Favoris&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Favoris&quot;.</value>
		public static StockIcon Favorites
		{
			get { return GetStockIcon("Favorites"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Rechercher&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Rechercher&quot;.</value>
		public static StockIcon Find
		{
			get { return GetStockIcon("Find"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Drapeau&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Drapeau&quot;.</value>
		public static StockIcon Flag
		{
			get { return GetStockIcon("Flag"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Plein écran&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Plein écran&quot;.</value>
		public static StockIcon FullScreen
		{
			get { return GetStockIcon("FullScreen"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Aide&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Aide&quot;.</value>
		public static StockIcon Help
		{
			get { return GetStockIcon("Help"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Historique&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Historique&quot;.</value>
		public static StockIcon History
		{
			get { return GetStockIcon("History"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Page de démarrage&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Page de démarrage&quot;.</value>
		public static StockIcon Home
		{
			get { return GetStockIcon("Home"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Information&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Information&quot;.</value>
		public static StockIcon Information
		{
			get { return GetStockIcon("Information"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Internet&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Internet&quot;.</value>
		public static StockIcon Internet
		{
			get { return GetStockIcon("Internet"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Nouveau&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Nouveau&quot;.</value>
		public static StockIcon New
		{
			get { return GetStockIcon("New"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Suivant&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Suivant&quot;.</value>
		public static StockIcon Next
		{
			get { return GetStockIcon("Next"); }
		}
	
		/// <summary>
		/// Obtient l'icône standard &quot;Ouvrir&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Ouvrir&quot;.</value>
		public static StockIcon Open
		{
			get { return GetStockIcon("Open"); }
		}
	
		/// <summary>
		/// Obtient l'icône standard &quot;Coller&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Coller&quot;.</value>
		public static StockIcon Paste
		{
			get { return GetStockIcon("Paste"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Aperçu&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Aperçu&quot;.</value>
		public static StockIcon Preview
		{
			get { return GetStockIcon("Preview"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Précédent&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Précédent&quot;.</value>
		public static StockIcon Previous
		{
			get { return GetStockIcon("Previous"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Imprimer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Imprimer&quot;.</value>
		public static StockIcon Print
		{
			get { return GetStockIcon("Print"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Propriétés&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Propriétés&quot;.</value>
		public static StockIcon Properties
		{
			get { return GetStockIcon("Properties"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Question&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Question&quot;.</value>
		public static StockIcon Question
		{
			get { return GetStockIcon("Question"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Rétablir&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Rétablir&quot;.</value>
		public static StockIcon Redo
		{
			get { return GetStockIcon("Redo"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Actualiser&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Actualiser&quot;.</value>
		public static StockIcon Refresh
		{
			get { return GetStockIcon("Refresh"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Redémarrer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Redémarrer&quot;.</value>
		public static StockIcon Restart
		{
			get { return GetStockIcon("Restart"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Exécuter&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Exécuter&quot;.</value>
		public static StockIcon Run
		{
			get { return GetStockIcon("Run"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Enregistrer&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Enregistrer&quot;.</value>
		public static StockIcon Save
		{
			get { return GetStockIcon("Save"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Enregistrer tout&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Enregistrer tout&quot;.</value>
		public static StockIcon SaveAll
		{
			get { return GetStockIcon("SaveAll"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Enregistrer sous&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Enregistrer sous&quot;.</value>
		public static StockIcon SaveAs
		{
			get { return GetStockIcon("SaveAs"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Sélectionner tout&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Sélectionner tout&quot;.</value>
		public static StockIcon SelectAll
		{
			get { return GetStockIcon("SelectAll"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Paramètres&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Paramètres&quot;.</value>
		public static StockIcon Settings
		{
			get { return GetStockIcon("Settings"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Bouclier&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Bouclier&quot;.</value>
		public static StockIcon Shield
		{
			get { return GetStockIcon("Shield"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Arrêter&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Arrêter&quot;.</value>
		public static StockIcon Stop
		{
			get { return GetStockIcon("Stop"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Succès&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Succès&quot;.</value>
		public static StockIcon Success
		{
			get { return GetStockIcon("Success"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Support&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Support&quot;.</value>
		public static StockIcon Support
		{
			get { return GetStockIcon("Support"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Astuce&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Astuce&quot;.</value>
		public static StockIcon Tip
		{
			get { return GetStockIcon("Tip"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Annuler&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Annuler&quot;.</value>
		public static StockIcon Undo
		{
			get { return GetStockIcon("Undo"); }
		}
		
		/// <summary>
		/// Obtient l'icône standard &quot;Avertissement&quot;.
		/// </summary>
		/// <value>L'icône standard &quot;Avertissement&quot;.</value>
		public static StockIcon Warning
		{
			get { return GetStockIcon("Warning"); }
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient l'icône correspondant au nom spécifié.
		/// </summary>
		/// <param name="name">Nom de l'icône.</param>
		/// <returns>Icône correspondant au nom spécifié.</returns>
		private static StockIcon GetStockIcon(string name)
		{
			if(!icons.ContainsKey(name)) icons[name]=new StockIcon((Icon) resources.GetObject(name)) { Name=name };
			return icons[name];
		}
	}
}