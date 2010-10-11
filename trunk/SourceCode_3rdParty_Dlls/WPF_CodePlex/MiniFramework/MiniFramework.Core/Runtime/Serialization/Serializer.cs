// <copyright file="Serializer.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Runtime.Serialization.Serializer</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Runtime.Serialization
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Text;
	using System.Web.Script.Serialization;
	using System.Xml.Serialization;

	//// ========================================================================================

	/// <summary>
	/// Sérialise et désérialise des objets.
	/// </summary>
	/// <remarks>
	/// La sérialisation est le processus de conversion d'un objet ou d'un graphique d'objets en séquence linéaire d'octets pour stockage ou transmission à un autre emplacement. La désérialisation est le processus consistant à prendre des informations stockées et à recréer des objets à partir de celles-ci.
	/// </remarks>
	public static class Serializer
	{
		/// <summary>
		/// Désérialise la séquence d'octets spécifiée.
		/// </summary>
		/// <param name="data">Séquence d'octets à désérialiser.</param>
		/// <typeparam name="T">Type de l'objet retourné.</typeparam>
		/// <returns>Objet correspondant à la séquence d'octets désérialisée.</returns>
		/// <exception cref="ArgumentNullException">Le tableau spécifié est une référence null.</exception>
		public static T Deserialize<T>(byte[] data)
		{
			if(data==null) throw new ArgumentNullException("data");
			
			using(var stream=new MemoryStream(data.Length))
			{
				stream.Write(data, 0, data.Length);
				stream.Seek(0, SeekOrigin.Begin);
				return (T) new BinaryFormatter().Deserialize(stream);
			}
		}

		/// <summary>
		/// Sérialise l'objet spécifié sous forme de séquence d'octets.
		/// </summary>
		/// <param name="value">Objet à sérialiser.</param>
		/// <returns>Séquence d'octets correspondant à l'objet sérialisé.</returns>
		public static byte[] Serialize(object value)
		{
			using(var stream=new MemoryStream())
			{
				new BinaryFormatter().Serialize(stream, value);
				return stream.ToArray();
			}
		}

		//// =====================================================================================
		
		/// <summary>
		/// Désérialise la notation JavaScript (JSON) spécifiée.
		/// </summary>
		/// <param name="json">Notation JavaScript à désérialiser.</param>
		/// <typeparam name="T">Type de l'objet retourné.</typeparam>
		/// <returns>Objet correspondant à la notation JavaScript désérialisée.</returns>
		public static T DeserializeFromJson<T>(string json)
		{
			return new JavaScriptSerializer().Deserialize<T>(json);
		}

		/// <summary>
		/// Sérialise l'objet spécifié sous forme de notation JavaScript (JSON).
		/// </summary>
		/// <param name="value">Objet à sérialiser.</param>
		/// <returns>Notation JavaScript correspondant à l'objet sérialisé.</returns>
		public static string SerializeToJson(object value)
		{
			return new JavaScriptSerializer().Serialize(value);
		}

		//// =====================================================================================
		
		/// <summary>
		/// Désérialise le document XML spécifié.
		/// </summary>
		/// <param name="xml">Document XML à désérialiser.</param>
		/// <typeparam name="T">Type de l'objet retourné.</typeparam>
		/// <returns>Objet correspondant au document XML désérialisé.</returns>
		public static T DeserializeFromXml<T>(string xml)
		{
			var data=Encoding.UTF8.GetBytes(xml);
			using(var stream=new MemoryStream(data.Length))
			{
				stream.Write(data, 0, data.Length);
				stream.Seek(0, SeekOrigin.Begin);
				return (T) new XmlSerializer(typeof(T)).Deserialize(stream);
			}
		}

		/// <summary>
		/// Sérialise l'objet spécifié sous forme de document XML.
		/// </summary>
		/// <param name="value">Objet à sérialiser.</param>
		/// <returns>Document XML correspondant à l'objet sérialisé.</returns>
		/// <exception cref="ArgumentNullException">L'objet spécifié est une référence null.</exception>
		public static string SerializeToXml(object value)
		{
			if(value==null) throw new ArgumentNullException("value");
			
			using(var stream=new MemoryStream())
			{
				new XmlSerializer(value.GetType()).Serialize(stream, value);
				return Encoding.UTF8.GetString(stream.ToArray());
			}
		}
	}
}