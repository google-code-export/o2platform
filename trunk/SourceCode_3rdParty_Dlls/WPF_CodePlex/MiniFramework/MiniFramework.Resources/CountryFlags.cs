// <copyright file="CountryFlags.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Resources.CountryFlags</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 11:16:06 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2020 $</version>

namespace MiniFramework.Resources
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Globalization;
	using System.Linq;
	using System.Reflection;
	using System.Resources;

	using MiniFramework.Drawing;
	using MiniFramework.Properties;
	using MiniFramework.Reflection;

	//// ========================================================================================

	/// <summary>
	/// Fournit une collection d'objets <see cref="StockIcon" /> représentant des drapeaux de pays.
	/// </summary>
	public static class CountryFlags
	{
		/// <summary>
		/// Instances des icônes mises en cache.
		/// </summary>
		private static Dictionary<string, StockIcon> icons=new Dictionary<string, StockIcon>();

		/// <summary>
		/// Gestionnaire utilisé pour accéder aux ressources de la classe.
		/// </summary>
		private static ResourceManager resources=new ResourceManager(typeof(CountryFlags));

		//// =====================================================================================
		
		/// <summary>
		/// Obtient le drapeau de pays &quot;AD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AD&quot;.</value>
		public static StockIcon AD
		{
			get { return GetStockIcon("ad"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;E.A.U. (AE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;E.A.U. (AE)&quot;.</value>
		public static StockIcon AE
		{
			get { return GetStockIcon("ae"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AF&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AF&quot;.</value>
		public static StockIcon AF
		{
			get { return GetStockIcon("af"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AG&quot;.</value>
		public static StockIcon AG
		{
			get { return GetStockIcon("ag"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AI&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AI&quot;.</value>
		public static StockIcon AI
		{
			get { return GetStockIcon("ai"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Albanie (AL)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Albanie (AL)&quot;.</value>
		public static StockIcon AL
		{
			get { return GetStockIcon("al"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Arménie (AM)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Arménie (AM)&quot;.</value>
		public static StockIcon AM
		{
			get { return GetStockIcon("am"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AN&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AN&quot;.</value>
		public static StockIcon AN
		{
			get { return GetStockIcon("an"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AO&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AO&quot;.</value>
		public static StockIcon AO
		{
			get { return GetStockIcon("ao"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AQ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AQ&quot;.</value>
		public static StockIcon AQ
		{
			get { return GetStockIcon("aq"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Argentine (AR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Argentine (AR)&quot;.</value>
		public static StockIcon AR
		{
			get { return GetStockIcon("ar"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AS&quot;.</value>
		public static StockIcon AS
		{
			get { return GetStockIcon("as"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Autriche (AT)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Autriche (AT)&quot;.</value>
		public static StockIcon AT
		{
			get { return GetStockIcon("at"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Australie (AU)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Australie (AU)&quot;.</value>
		public static StockIcon AU
		{
			get { return GetStockIcon("au"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;AW&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;AW&quot;.</value>
		public static StockIcon AW
		{
			get { return GetStockIcon("aw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Azerbaïdjan (AZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Azerbaïdjan (AZ)&quot;.</value>
		public static StockIcon AZ
		{
			get { return GetStockIcon("az"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BA&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BA&quot;.</value>
		public static StockIcon BA
		{
			get { return GetStockIcon("ba"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BB&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BB&quot;.</value>
		public static StockIcon BB
		{
			get { return GetStockIcon("bb"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BD&quot;.</value>
		public static StockIcon BD
		{
			get { return GetStockIcon("bd"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Belgique (BE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Belgique (BE)&quot;.</value>
		public static StockIcon BE
		{
			get { return GetStockIcon("be"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BF&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BF&quot;.</value>
		public static StockIcon BF
		{
			get { return GetStockIcon("bf"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Bulgarie (BG)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Bulgarie (BG)&quot;.</value>
		public static StockIcon BG
		{
			get { return GetStockIcon("bg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Bahreïn (BH)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Bahreïn (BH)&quot;.</value>
		public static StockIcon BH
		{
			get { return GetStockIcon("bh"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BI&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BI&quot;.</value>
		public static StockIcon BI
		{
			get { return GetStockIcon("bi"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BJ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BJ&quot;.</value>
		public static StockIcon BJ
		{
			get { return GetStockIcon("bj"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BM&quot;.</value>
		public static StockIcon BM
		{
			get { return GetStockIcon("bm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Brunei Darussalam (BN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Brunei Darussalam (BN)&quot;.</value>
		public static StockIcon BN
		{
			get { return GetStockIcon("bn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Bolivie (BO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Bolivie (BO)&quot;.</value>
		public static StockIcon BO
		{
			get { return GetStockIcon("bo"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Brésil (BR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Brésil (BR)&quot;.</value>
		public static StockIcon BR
		{
			get { return GetStockIcon("br"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BS&quot;.</value>
		public static StockIcon BS
		{
			get { return GetStockIcon("bs"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BT&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BT&quot;.</value>
		public static StockIcon BT
		{
			get { return GetStockIcon("bt"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;BW&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;BW&quot;.</value>
		public static StockIcon BW
		{
			get { return GetStockIcon("bw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Bélarus (BY)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Bélarus (BY)&quot;.</value>
		public static StockIcon BY
		{
			get { return GetStockIcon("by"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Belize (BZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Belize (BZ)&quot;.</value>
		public static StockIcon BZ
		{
			get { return GetStockIcon("bz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Canada (CA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Canada (CA)&quot;.</value>
		public static StockIcon CA
		{
			get { return GetStockIcon("ca"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CD&quot;.</value>
		public static StockIcon CD
		{
			get { return GetStockIcon("cd"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CF&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CF&quot;.</value>
		public static StockIcon CF
		{
			get { return GetStockIcon("cf"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CG&quot;.</value>
		public static StockIcon CG
		{
			get { return GetStockIcon("cg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Suisse (CH)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Suisse (CH)&quot;.</value>
		public static StockIcon CH
		{
			get { return GetStockIcon("ch"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CI&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CI&quot;.</value>
		public static StockIcon CI
		{
			get { return GetStockIcon("ci"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CK&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CK&quot;.</value>
		public static StockIcon CK
		{
			get { return GetStockIcon("ck"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Chili (CL)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Chili (CL)&quot;.</value>
		public static StockIcon CL
		{
			get { return GetStockIcon("cl"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CM&quot;.</value>
		public static StockIcon CM
		{
			get { return GetStockIcon("cm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;République populaire de Chine (CN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;République populaire de Chine (CN)&quot;.</value>
		public static StockIcon CN
		{
			get { return GetStockIcon("cn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Colombie (CO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Colombie (CO)&quot;.</value>
		public static StockIcon CO
		{
			get { return GetStockIcon("co"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Costa Rica (CR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Costa Rica (CR)&quot;.</value>
		public static StockIcon CR
		{
			get { return GetStockIcon("cr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CU&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CU&quot;.</value>
		public static StockIcon CU
		{
			get { return GetStockIcon("cu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CV&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CV&quot;.</value>
		public static StockIcon CV
		{
			get { return GetStockIcon("cv"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;CY&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;CY&quot;.</value>
		public static StockIcon CY
		{
			get { return GetStockIcon("cy"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;République tchèque (CZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;République tchèque (CZ)&quot;.</value>
		public static StockIcon CZ
		{
			get { return GetStockIcon("cz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Allemagne (DE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Allemagne (DE)&quot;.</value>
		public static StockIcon DE
		{
			get { return GetStockIcon("de"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;DJ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;DJ&quot;.</value>
		public static StockIcon DJ
		{
			get { return GetStockIcon("dj"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Danemark (DK)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Danemark (DK)&quot;.</value>
		public static StockIcon DK
		{
			get { return GetStockIcon("dk"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;DM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;DM&quot;.</value>
		public static StockIcon DM
		{
			get { return GetStockIcon("dm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;République dominicaine (DO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;République dominicaine (DO)&quot;.</value>
		public static StockIcon DO
		{
			get { return GetStockIcon("do"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Algérie (DZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Algérie (DZ)&quot;.</value>
		public static StockIcon DZ
		{
			get { return GetStockIcon("dz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Équateur (République de l') (EC)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Équateur (République de l') (EC)&quot;.</value>
		public static StockIcon EC
		{
			get { return GetStockIcon("ec"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Estonie (EE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Estonie (EE)&quot;.</value>
		public static StockIcon EE
		{
			get { return GetStockIcon("ee"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Égypte (EG)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Égypte (EG)&quot;.</value>
		public static StockIcon EG
		{
			get { return GetStockIcon("eg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;EH&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;EH&quot;.</value>
		public static StockIcon EH
		{
			get { return GetStockIcon("eh"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;ER&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;ER&quot;.</value>
		public static StockIcon ER
		{
			get { return GetStockIcon("er"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Espagne (ES)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Espagne (ES)&quot;.</value>
		public static StockIcon ES
		{
			get { return GetStockIcon("es"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;ET&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;ET&quot;.</value>
		public static StockIcon ET
		{
			get { return GetStockIcon("et"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;EU&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;EU&quot;.</value>
		public static StockIcon EU
		{
			get { return GetStockIcon("eu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Finlande (FI)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Finlande (FI)&quot;.</value>
		public static StockIcon FI
		{
			get { return GetStockIcon("fi"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;FJ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;FJ&quot;.</value>
		public static StockIcon FJ
		{
			get { return GetStockIcon("fj"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;FM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;FM&quot;.</value>
		public static StockIcon FM
		{
			get { return GetStockIcon("fm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Féroé (îles) (FO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Féroé (îles) (FO)&quot;.</value>
		public static StockIcon FO
		{
			get { return GetStockIcon("fo"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;France (FR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;France (FR)&quot;.</value>
		public static StockIcon FR
		{
			get { return GetStockIcon("fr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GA&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GA&quot;.</value>
		public static StockIcon GA
		{
			get { return GetStockIcon("ga"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Royaume-Uni (GB)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Royaume-Uni (GB)&quot;.</value>
		public static StockIcon GB
		{
			get { return GetStockIcon("gb"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GD&quot;.</value>
		public static StockIcon GD
		{
			get { return GetStockIcon("gd"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Géorgie (GE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Géorgie (GE)&quot;.</value>
		public static StockIcon GE
		{
			get { return GetStockIcon("ge"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GG&quot;.</value>
		public static StockIcon GG
		{
			get { return GetStockIcon("gg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GH&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GH&quot;.</value>
		public static StockIcon GH
		{
			get { return GetStockIcon("gh"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GI&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GI&quot;.</value>
		public static StockIcon GI
		{
			get { return GetStockIcon("gi"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GL&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GL&quot;.</value>
		public static StockIcon GL
		{
			get { return GetStockIcon("gl"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GM&quot;.</value>
		public static StockIcon GM
		{
			get { return GetStockIcon("gm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GN&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GN&quot;.</value>
		public static StockIcon GN
		{
			get { return GetStockIcon("gn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GP&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GP&quot;.</value>
		public static StockIcon GP
		{
			get { return GetStockIcon("gp"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GQ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GQ&quot;.</value>
		public static StockIcon GQ
		{
			get { return GetStockIcon("gq"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Grèce (GR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Grèce (GR)&quot;.</value>
		public static StockIcon GR
		{
			get { return GetStockIcon("gr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Guatemala (GT)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Guatemala (GT)&quot;.</value>
		public static StockIcon GT
		{
			get { return GetStockIcon("gt"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GU&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GU&quot;.</value>
		public static StockIcon GU
		{
			get { return GetStockIcon("gu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GW&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GW&quot;.</value>
		public static StockIcon GW
		{
			get { return GetStockIcon("gw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;GY&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;GY&quot;.</value>
		public static StockIcon GY
		{
			get { return GetStockIcon("gy"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Hong Kong RAS (HK)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Hong Kong RAS (HK)&quot;.</value>
		public static StockIcon HK
		{
			get { return GetStockIcon("hk"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Honduras (HN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Honduras (HN)&quot;.</value>
		public static StockIcon HN
		{
			get { return GetStockIcon("hn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Croatie (HR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Croatie (HR)&quot;.</value>
		public static StockIcon HR
		{
			get { return GetStockIcon("hr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;HT&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;HT&quot;.</value>
		public static StockIcon HT
		{
			get { return GetStockIcon("ht"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Hongrie (HU)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Hongrie (HU)&quot;.</value>
		public static StockIcon HU
		{
			get { return GetStockIcon("hu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Indonésie (ID)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Indonésie (ID)&quot;.</value>
		public static StockIcon ID
		{
			get { return GetStockIcon("id"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Irlande (IE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Irlande (IE)&quot;.</value>
		public static StockIcon IE
		{
			get { return GetStockIcon("ie"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Israël (IL)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Israël (IL)&quot;.</value>
		public static StockIcon IL
		{
			get { return GetStockIcon("il"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;IM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;IM&quot;.</value>
		public static StockIcon IM
		{
			get { return GetStockIcon("im"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Inde (IN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Inde (IN)&quot;.</value>
		public static StockIcon IN
		{
			get { return GetStockIcon("in"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Irak (IQ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Irak (IQ)&quot;.</value>
		public static StockIcon IQ
		{
			get { return GetStockIcon("iq"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Iran (IR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Iran (IR)&quot;.</value>
		public static StockIcon IR
		{
			get { return GetStockIcon("ir"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Islande (IS)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Islande (IS)&quot;.</value>
		public static StockIcon IS
		{
			get { return GetStockIcon("is"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Italie (IT)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Italie (IT)&quot;.</value>
		public static StockIcon IT
		{
			get { return GetStockIcon("it"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;JE&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;JE&quot;.</value>
		public static StockIcon JE
		{
			get { return GetStockIcon("je"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Jamaïque (JM)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Jamaïque (JM)&quot;.</value>
		public static StockIcon JM
		{
			get { return GetStockIcon("jm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Jordanie (JO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Jordanie (JO)&quot;.</value>
		public static StockIcon JO
		{
			get { return GetStockIcon("jo"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Japon (JP)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Japon (JP)&quot;.</value>
		public static StockIcon JP
		{
			get { return GetStockIcon("jp"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Kenya (KE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Kenya (KE)&quot;.</value>
		public static StockIcon KE
		{
			get { return GetStockIcon("ke"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Kirghizistan (KG)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Kirghizistan (KG)&quot;.</value>
		public static StockIcon KG
		{
			get { return GetStockIcon("kg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;KH&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;KH&quot;.</value>
		public static StockIcon KH
		{
			get { return GetStockIcon("kh"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;KI&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;KI&quot;.</value>
		public static StockIcon KI
		{
			get { return GetStockIcon("ki"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;KM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;KM&quot;.</value>
		public static StockIcon KM
		{
			get { return GetStockIcon("km"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;KN&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;KN&quot;.</value>
		public static StockIcon KN
		{
			get { return GetStockIcon("kn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;KP&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;KP&quot;.</value>
		public static StockIcon KP
		{
			get { return GetStockIcon("kp"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Corée (KR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Corée (KR)&quot;.</value>
		public static StockIcon KR
		{
			get { return GetStockIcon("kr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Koweït (KW)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Koweït (KW)&quot;.</value>
		public static StockIcon KW
		{
			get { return GetStockIcon("kw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;KY&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;KY&quot;.</value>
		public static StockIcon KY
		{
			get { return GetStockIcon("ky"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Kazakhstan (KZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Kazakhstan (KZ)&quot;.</value>
		public static StockIcon KZ
		{
			get { return GetStockIcon("kz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;LA&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;LA&quot;.</value>
		public static StockIcon LA
		{
			get { return GetStockIcon("la"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Liban (LB)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Liban (LB)&quot;.</value>
		public static StockIcon LB
		{
			get { return GetStockIcon("lb"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;LC&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;LC&quot;.</value>
		public static StockIcon LC
		{
			get { return GetStockIcon("lc"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Liechtenstein (LI)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Liechtenstein (LI)&quot;.</value>
		public static StockIcon LI
		{
			get { return GetStockIcon("li"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;LK&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;LK&quot;.</value>
		public static StockIcon LK
		{
			get { return GetStockIcon("lk"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;LR&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;LR&quot;.</value>
		public static StockIcon LR
		{
			get { return GetStockIcon("lr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;LS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;LS&quot;.</value>
		public static StockIcon LS
		{
			get { return GetStockIcon("ls"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Lituanie (LT)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Lituanie (LT)&quot;.</value>
		public static StockIcon LT
		{
			get { return GetStockIcon("lt"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Luxembourg (LU)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Luxembourg (LU)&quot;.</value>
		public static StockIcon LU
		{
			get { return GetStockIcon("lu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Lettonie (LV)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Lettonie (LV)&quot;.</value>
		public static StockIcon LV
		{
			get { return GetStockIcon("lv"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Libye (LY)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Libye (LY)&quot;.</value>
		public static StockIcon LY
		{
			get { return GetStockIcon("ly"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Maroc (MA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Maroc (MA)&quot;.</value>
		public static StockIcon MA
		{
			get { return GetStockIcon("ma"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Principauté de Monaco (MC)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Principauté de Monaco (MC)&quot;.</value>
		public static StockIcon MC
		{
			get { return GetStockIcon("mc"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MD&quot;.</value>
		public static StockIcon MD
		{
			get { return GetStockIcon("md"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;ME&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;ME&quot;.</value>
		public static StockIcon ME
		{
			get { return GetStockIcon("me"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MG&quot;.</value>
		public static StockIcon MG
		{
			get { return GetStockIcon("mg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MH&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MH&quot;.</value>
		public static StockIcon MH
		{
			get { return GetStockIcon("mh"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Macédoine (Ex-République yougoslave de Macédoine) (MK)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Macédoine (Ex-République yougoslave de Macédoine) (MK)&quot;.</value>
		public static StockIcon MK
		{
			get { return GetStockIcon("mk"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;ML&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;ML&quot;.</value>
		public static StockIcon ML
		{
			get { return GetStockIcon("ml"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MM&quot;.</value>
		public static StockIcon MM
		{
			get { return GetStockIcon("mm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Mongolie (MN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Mongolie (MN)&quot;.</value>
		public static StockIcon MN
		{
			get { return GetStockIcon("mn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Macao RAS (MO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Macao RAS (MO)&quot;.</value>
		public static StockIcon MO
		{
			get { return GetStockIcon("mo"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MQ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MQ&quot;.</value>
		public static StockIcon MQ
		{
			get { return GetStockIcon("mq"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MR&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MR&quot;.</value>
		public static StockIcon MR
		{
			get { return GetStockIcon("mr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MS&quot;.</value>
		public static StockIcon MS
		{
			get { return GetStockIcon("ms"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MT&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MT&quot;.</value>
		public static StockIcon MT
		{
			get { return GetStockIcon("mt"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MU&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MU&quot;.</value>
		public static StockIcon MU
		{
			get { return GetStockIcon("mu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Maldives (MV)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Maldives (MV)&quot;.</value>
		public static StockIcon MV
		{
			get { return GetStockIcon("mv"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MW&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MW&quot;.</value>
		public static StockIcon MW
		{
			get { return GetStockIcon("mw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Mexique (MX)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Mexique (MX)&quot;.</value>
		public static StockIcon MX
		{
			get { return GetStockIcon("mx"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Malaisie (MY)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Malaisie (MY)&quot;.</value>
		public static StockIcon MY
		{
			get { return GetStockIcon("my"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;MZ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;MZ&quot;.</value>
		public static StockIcon MZ
		{
			get { return GetStockIcon("mz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;NA&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;NA&quot;.</value>
		public static StockIcon NA
		{
			get { return GetStockIcon("na"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;NC&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;NC&quot;.</value>
		public static StockIcon NC
		{
			get { return GetStockIcon("nc"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;NE&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;NE&quot;.</value>
		public static StockIcon NE
		{
			get { return GetStockIcon("ne"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;NG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;NG&quot;.</value>
		public static StockIcon NG
		{
			get { return GetStockIcon("ng"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Nicaragua (NI)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Nicaragua (NI)&quot;.</value>
		public static StockIcon NI
		{
			get { return GetStockIcon("ni"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Pays-Bas (NL)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Pays-Bas (NL)&quot;.</value>
		public static StockIcon NL
		{
			get { return GetStockIcon("nl"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Norvège (NO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Norvège (NO)&quot;.</value>
		public static StockIcon NO
		{
			get { return GetStockIcon("no"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;NP&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;NP&quot;.</value>
		public static StockIcon NP
		{
			get { return GetStockIcon("np"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;NR&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;NR&quot;.</value>
		public static StockIcon NR
		{
			get { return GetStockIcon("nr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Nouvelle-Zélande (NZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Nouvelle-Zélande (NZ)&quot;.</value>
		public static StockIcon NZ
		{
			get { return GetStockIcon("nz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Oman (OM)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Oman (OM)&quot;.</value>
		public static StockIcon OM
		{
			get { return GetStockIcon("om"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Panama (PA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Panama (PA)&quot;.</value>
		public static StockIcon PA
		{
			get { return GetStockIcon("pa"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Pérou (PE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Pérou (PE)&quot;.</value>
		public static StockIcon PE
		{
			get { return GetStockIcon("pe"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;PF&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;PF&quot;.</value>
		public static StockIcon PF
		{
			get { return GetStockIcon("pf"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;PG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;PG&quot;.</value>
		public static StockIcon PG
		{
			get { return GetStockIcon("pg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;République des Philippines (PH)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;République des Philippines (PH)&quot;.</value>
		public static StockIcon PH
		{
			get { return GetStockIcon("ph"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;République islamique du Pakistan (PK)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;République islamique du Pakistan (PK)&quot;.</value>
		public static StockIcon PK
		{
			get { return GetStockIcon("pk"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Pologne (PL)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Pologne (PL)&quot;.</value>
		public static StockIcon PL
		{
			get { return GetStockIcon("pl"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Porto Rico (PR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Porto Rico (PR)&quot;.</value>
		public static StockIcon PR
		{
			get { return GetStockIcon("pr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;PS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;PS&quot;.</value>
		public static StockIcon PS
		{
			get { return GetStockIcon("ps"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Portugal (PT)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Portugal (PT)&quot;.</value>
		public static StockIcon PT
		{
			get { return GetStockIcon("pt"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;PW&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;PW&quot;.</value>
		public static StockIcon PW
		{
			get { return GetStockIcon("pw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Paraguay (PY)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Paraguay (PY)&quot;.</value>
		public static StockIcon PY
		{
			get { return GetStockIcon("py"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Qatar (QA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Qatar (QA)&quot;.</value>
		public static StockIcon QA
		{
			get { return GetStockIcon("qa"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;RE&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;RE&quot;.</value>
		public static StockIcon RE
		{
			get { return GetStockIcon("re"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Roumanie (RO)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Roumanie (RO)&quot;.</value>
		public static StockIcon RO
		{
			get { return GetStockIcon("ro"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;RS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;RS&quot;.</value>
		public static StockIcon RS
		{
			get { return GetStockIcon("rs"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Russie (RU)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Russie (RU)&quot;.</value>
		public static StockIcon RU
		{
			get { return GetStockIcon("ru"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;RW&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;RW&quot;.</value>
		public static StockIcon RW
		{
			get { return GetStockIcon("rw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Arabie saoudite (SA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Arabie saoudite (SA)&quot;.</value>
		public static StockIcon SA
		{
			get { return GetStockIcon("sa"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SB&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SB&quot;.</value>
		public static StockIcon SB
		{
			get { return GetStockIcon("sb"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SC&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SC&quot;.</value>
		public static StockIcon SC
		{
			get { return GetStockIcon("sc"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SD&quot;.</value>
		public static StockIcon SD
		{
			get { return GetStockIcon("sd"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Suède (SE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Suède (SE)&quot;.</value>
		public static StockIcon SE
		{
			get { return GetStockIcon("se"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Singapour (SG)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Singapour (SG)&quot;.</value>
		public static StockIcon SG
		{
			get { return GetStockIcon("sg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Slovénie (SI)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Slovénie (SI)&quot;.</value>
		public static StockIcon SI
		{
			get { return GetStockIcon("si"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Slovaquie (SK)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Slovaquie (SK)&quot;.</value>
		public static StockIcon SK
		{
			get { return GetStockIcon("sk"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SL&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SL&quot;.</value>
		public static StockIcon SL
		{
			get { return GetStockIcon("sl"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SM&quot;.</value>
		public static StockIcon SM
		{
			get { return GetStockIcon("sm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SN&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SN&quot;.</value>
		public static StockIcon SN
		{
			get { return GetStockIcon("sn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SO&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SO&quot;.</value>
		public static StockIcon SO
		{
			get { return GetStockIcon("so"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SR&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SR&quot;.</value>
		public static StockIcon SR
		{
			get { return GetStockIcon("sr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;ST&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;ST&quot;.</value>
		public static StockIcon ST
		{
			get { return GetStockIcon("st"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Salvador (SV)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Salvador (SV)&quot;.</value>
		public static StockIcon SV
		{
			get { return GetStockIcon("sv"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Syrie (SY)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Syrie (SY)&quot;.</value>
		public static StockIcon SY
		{
			get { return GetStockIcon("sy"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;SZ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;SZ&quot;.</value>
		public static StockIcon SZ
		{
			get { return GetStockIcon("sz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TC&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TC&quot;.</value>
		public static StockIcon TC
		{
			get { return GetStockIcon("tc"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TD&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TD&quot;.</value>
		public static StockIcon TD
		{
			get { return GetStockIcon("td"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TG&quot;.</value>
		public static StockIcon TG
		{
			get { return GetStockIcon("tg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Thaïlande (TH)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Thaïlande (TH)&quot;.</value>
		public static StockIcon TH
		{
			get { return GetStockIcon("th"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TJ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TJ&quot;.</value>
		public static StockIcon TJ
		{
			get { return GetStockIcon("tj"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TL&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TL&quot;.</value>
		public static StockIcon TL
		{
			get { return GetStockIcon("tl"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TM&quot;.</value>
		public static StockIcon TM
		{
			get { return GetStockIcon("tm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Tunisie (TN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Tunisie (TN)&quot;.</value>
		public static StockIcon TN
		{
			get { return GetStockIcon("tn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TO&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TO&quot;.</value>
		public static StockIcon TO
		{
			get { return GetStockIcon("to"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Turquie (TR)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Turquie (TR)&quot;.</value>
		public static StockIcon TR
		{
			get { return GetStockIcon("tr"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Trinité-et-Tobago (TT)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Trinité-et-Tobago (TT)&quot;.</value>
		public static StockIcon TT
		{
			get { return GetStockIcon("tt"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TV&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TV&quot;.</value>
		public static StockIcon TV
		{
			get { return GetStockIcon("tv"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Taïwan (TW)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Taïwan (TW)&quot;.</value>
		public static StockIcon TW
		{
			get { return GetStockIcon("tw"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;TZ&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;TZ&quot;.</value>
		public static StockIcon TZ
		{
			get { return GetStockIcon("tz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Ukraine (UA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Ukraine (UA)&quot;.</value>
		public static StockIcon UA
		{
			get { return GetStockIcon("ua"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;UG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;UG&quot;.</value>
		public static StockIcon UG
		{
			get { return GetStockIcon("ug"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;États-Unis (US)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;États-Unis (US)&quot;.</value>
		public static StockIcon US
		{
			get { return GetStockIcon("us"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Uruguay (UY)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Uruguay (UY)&quot;.</value>
		public static StockIcon UY
		{
			get { return GetStockIcon("uy"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Ouzbékistan (UZ)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Ouzbékistan (UZ)&quot;.</value>
		public static StockIcon UZ
		{
			get { return GetStockIcon("uz"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;VA&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;VA&quot;.</value>
		public static StockIcon VA
		{
			get { return GetStockIcon("va"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;VC&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;VC&quot;.</value>
		public static StockIcon VC
		{
			get { return GetStockIcon("vc"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Venezuela (VE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Venezuela (VE)&quot;.</value>
		public static StockIcon VE
		{
			get { return GetStockIcon("ve"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;VG&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;VG&quot;.</value>
		public static StockIcon VG
		{
			get { return GetStockIcon("vg"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;VI&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;VI&quot;.</value>
		public static StockIcon VI
		{
			get { return GetStockIcon("vi"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Vietnam (VN)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Vietnam (VN)&quot;.</value>
		public static StockIcon VN
		{
			get { return GetStockIcon("vn"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;VU&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;VU&quot;.</value>
		public static StockIcon VU
		{
			get { return GetStockIcon("vu"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;WS&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;WS&quot;.</value>
		public static StockIcon WS
		{
			get { return GetStockIcon("ws"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Yémen (YE)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Yémen (YE)&quot;.</value>
		public static StockIcon YE
		{
			get { return GetStockIcon("ye"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Afrique du Sud (ZA)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Afrique du Sud (ZA)&quot;.</value>
		public static StockIcon ZA
		{
			get { return GetStockIcon("za"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;ZM&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;ZM&quot;.</value>
		public static StockIcon ZM
		{
			get { return GetStockIcon("zm"); }
		}

		/// <summary>
		/// Obtient le drapeau de pays &quot;Zimbabwe (ZW)&quot;.
		/// </summary>
		/// <value>Le drapeau de pays &quot;Zimbabwe (ZW)&quot;.</value>
		public static StockIcon ZW
		{
			get { return GetStockIcon("zw"); }
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient le drapeau de pays correspondant à la culture spécifiée.
		/// </summary>
		/// <param name="culture">Culture dont on extrait le drapeau de pays.</param>
		/// <returns>Drapeau de pays correspondant à la culture spécifiée, ou une référence null si aucune icône ne correspond.</returns>
		/// <exception cref="ArgumentNullException">La culture spécifiée est une référence null.</exception>
		public static StockIcon GetCountryFlag(this CultureInfo culture)
		{
			if(culture==null) throw new ArgumentNullException("culture");
			return GetCountryFlag(new RegionInfo(culture.LCID));
		}

		/// <summary>
		/// Obtient le drapeau de pays correspondant à la région spécifiée.
		/// </summary>
		/// <param name="region">Région dont on extrait le drapeau de pays.</param>
		/// <returns>Drapeau de pays correspondant à la région spécifiée, ou une référence null si aucune icône ne correspond.</returns>
		/// <exception cref="ArgumentNullException">La région spécifiée est une référence null.</exception>
		public static StockIcon GetCountryFlag(this RegionInfo region)
		{
			if(region==null) throw new ArgumentNullException("region");
			return GetCountryFlag(region.TwoLetterISORegionName);
		}

		/// <summary>
		/// Obtient le drapeau de pays correspondant au code ISO 3166 spécifié.
		/// </summary>
		/// <param name="isoCode">Code de région à deux lettres définis dans la norme ISO 3166.</param>
		/// <returns>Drapeau de pays correspondant au code ISO 3166 spécifié, ou une référence null si aucune icône ne correspond.</returns>
		/// <exception cref="ArgumentException">Le code ISO spécifié est une chaîne vide ou une référence null.</exception>
		public static StockIcon GetCountryFlag(string isoCode)
		{
			if(string.IsNullOrEmpty(isoCode)) throw new ArgumentException(Resources.EmptyIsoCode, "isoCode");
			try { return (StockIcon) Reflector.GetPropertyValue(typeof(CountryFlags), isoCode.ToUpperInvariant()); }
			catch(TargetException) { return null; }
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