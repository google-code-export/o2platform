// <copyright file="LimitedQueue.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPL) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Collections.LimitedQueue</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-29 00:13:12 +0200 (mar. 29 sept. 2009) $</date>
// <version>$Revision: 1927 $</version>

namespace MiniFramework.Collections
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	//// ========================================================================================

	/// <summary>
	/// Représente une collection d'objets &quot;premier entré, premier sorti&quot; (FIFO) dont le nombre d'éléments est limité.
	/// </summary>
	/// <typeparam name="T">Type des éléments de la queue.</typeparam>
	public class LimitedQueue<T>: Queue<T>
	{
		/// <summary>
		/// Nombre maximal d'éléments par défaut.
		/// </summary>
		private const int DefaultMaxItems=10;

		/// <summary>
		/// Nombre maximal d'éléments.
		/// </summary>
		private int maxItems;

		//// =====================================================================================
		
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="LimitedQueue{T}" />.
		/// </summary>
		public LimitedQueue(): this(DefaultMaxItems) {}

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="LimitedQueue{T}" /> qui est vide et le nombre maximal d'éléments spécifié.
		/// </summary>
		/// <param name="capacity">Nombre maximal d'éléments que la queue peut contenir.</param>
		public LimitedQueue(int capacity): base(capacity)
		{
			this.MaxItems=capacity;
		}

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="LimitedQueue{T}" /> qui contient des éléments copiés de la liste spécifiée.
		/// </summary>
		/// <param name="list">Liste dont les éléments sont copiés dans cette queue.</param>
		public LimitedQueue(IEnumerable<T> list): base(list)
		{
			this.MaxItems=Math.Max(list.Count(), DefaultMaxItems);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit le nombre maximal d'éléments que peut contenir la queue.
		/// </summary>
		/// <remarks>
		/// Si le nombre maximal à définir est inférieur au nombre actuel d'éléments, les éléments en excédant situés au début de la queue sont supprimés.
		/// </remarks>
		/// <value>Nombre maximal d'éléments que peut contenir la queue. La valeur par défaut est 10.</value>
		/// <exception cref="ArgumentOutOfRangeException">Le nombre spécifié est inférieur à 1.</exception>
		public int MaxItems
		{
			get { return this.maxItems; }
			
			set
			{
				if(value<1) throw new ArgumentOutOfRangeException("value");
				
				this.maxItems=value;
				while(this.Count>this.maxItems) this.Dequeue();
			}
		}

		//// =====================================================================================

		/// <summary>
		/// Ajoute l'objet spécifié à la fin de la queue.
		/// </summary>
		/// <remarks>
		/// Lorsque le nombre maximal d'éléments est atteint, les éléments situés au début de la queue sont supprimés afin que leur nombre ne dépasse jamais <see cref="MaxItems" />.
		/// </remarks>
		/// <param name="item">Objet à ajouter.</param>
		public new void Enqueue(T item)
		{
			while(this.Count>=this.maxItems) this.Dequeue();
			base.Enqueue(item);
		}
	}
}