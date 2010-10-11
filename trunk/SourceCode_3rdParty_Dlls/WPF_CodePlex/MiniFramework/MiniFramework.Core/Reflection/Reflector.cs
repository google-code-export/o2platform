// <copyright file="Reflector.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Reflection.Reflector</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Reflection
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	
	using MiniFramework.Properties;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes statiques permettant d'invoquer des membres de types obtenus par la réflexion.
	/// </summary>
	public static class Reflector
	{
		/// <summary>
		/// Obtient le type appelant de la méthode précédemment invoquée.
		/// </summary>
		/// <remarks>
		/// Le type obtenu représente le type déclarant la méthode invoquant l'appelant de <see cref="CallingType" />.
		/// </remarks>
		/// <instance>Type appelant de la méthode précédemment invoquée.</instance>
		/// <exception cref="InvalidOperationException">La pile de la trace des appels ne comporte pas suffisamment de cadres.</exception>
		public static Type CallingType
		{
			get
			{
				var stackTrace=new StackTrace();
				if(stackTrace.FrameCount<3) throw new InvalidOperationException(Resources.NotEnoughtStackFrames);

				return stackTrace.GetFrame(2).GetMethod().DeclaringType;
			}
		}

		//// ========================================================================================

		/// <summary>
		/// Obtient la valeur de la propriété non indexée spécifiée de l'objet ou du type spécifié.
		/// </summary>
		/// <param name="instance">Objet ou type dont on veut obtenir la valeur d'une propriété.</param>
		/// <param name="name">Nom de la propriété à obtenir.</param>
		/// <returns>Objet représentant la valeur de la propriété appelée.</returns>
		public static object GetPropertyValue(object instance, string name)
		{
			var property=(PropertyInfo) GetMember(instance, name, MemberTypes.Property, null);
			return property.GetValue(instance, null);
		}

		/// <summary>
		/// Appelle la méthode correspondant à la liste d'arguments spécifiée sur l'objet ou le type spécifié.
		/// </summary>
		/// <param name="instance">Objet ou type dont on veut appeler une méthode.</param>
		/// <param name="name">Nom de la méthode à appeler.</param>
		/// <param name="parameters">Tableau contenant les paramètres à passer à la méthode appelée.</param>
		/// <returns>Objet représentant la valeur de retour de la méthode appelée.</returns>
		public static object InvokeMethod(object instance, string name, params object[] parameters)
		{
			var method=(MethodInfo) GetMember(instance, name, MemberTypes.Method, parameters.Select(x=>x.GetType()).ToArray());
			return method.Invoke(instance, parameters);
		}

		/// <summary>
		/// Définit la valeur de la propriété non indexée spécifiée de l'objet ou du type spécifié.
		/// </summary>
		/// <param name="instance">Objet ou type dont on veut définir la valeur d'une propriété.</param>
		/// <param name="name">Nom de la propriété à définir.</param>
		/// <param name="value">Valeur de la propriété à définir.</param>
		public static void SetPropertyValue(object instance, string name, object value)
		{
			var property=(PropertyInfo) GetMember(instance, name, MemberTypes.Property, null);
			property.SetValue(instance, value, null);
		}

		//// ========================================================================================

		/// <summary>
		/// Obtient le membre de l'objet ou du type spécifié correspondant au nom et au type de membre spécifiés.
		/// </summary>
		/// <param name="instance">Objet ou type dont on veut obtenir un membre.</param>
		/// <param name="memberName">Nom du membre à obtenir.</param>
		/// <param name="memberType">Valeur énumérée indiquant le type du membre à obtenir.</param>
		/// <param name="parameterTypes">Liste contenant les types des paramètres du membre à obtenir si celui-ci est une méthode, ou une référence null si le membre n'est pas une méthode.</param>
		/// <exception cref="AmbiguousMatchException">Il existe plusieurs membres correspondant au nom spécifié.</exception>
		/// <exception cref="ArgumentNullException">L'objet ou le type spécifié est une référence null.</exception>
		/// <exception cref="TargetException">Le membre spécifié est invalide.</exception>
		private static MemberInfo GetMember(object instance, string memberName, MemberTypes memberType, IList<Type> parameterTypes)
		{
			if(instance==null) throw new ArgumentNullException("instance");

			// Détermination du type déclarant le membre à obtenir
			var type=instance as Type;
			if(type==null) type=instance.GetType();

			// Obtention des membres correspondant au nom spécifié
			var members=type.GetMember(memberName, memberType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
			if(members.Length==0) throw new TargetException(Resources.InvalidMemberName);

			// Un seul membre trouvé : aucune ambiguïté, on peut le retourner
			if(members.Length==1) return members[0];

			// Plusieurs membres trouvés : on tente de résoudre l'ambiguïté
			if(members.Length>1 && parameterTypes!=null)
			{
				foreach(var member in members)
				{
					// Le membre doit être une méthode pour pouvoir essayer de lever l'ambiguïté
					var method=member as MethodInfo;
					if(method!=null)
					{
						// La méthode doit avoir le même nombre de paramètres que le nombre de types fournis
						var parameters=method.GetParameters();
						if(parameters.Length==parameterTypes.Count)
						{
							// Les paramètres de la méthode doivent avoir les mêmes types que ceux fournis
							var index=0;
							foreach(var parameter in parameters)
							{
								if(parameter.ParameterType!=parameterTypes[index]) break;
								index++;
							}
							
							if(index>=parameters.Length) return member;
						}
					}
				}
			}

			// L'ambiguïté n'a pas pu être résolue
			throw new AmbiguousMatchException();
		}
	}
}