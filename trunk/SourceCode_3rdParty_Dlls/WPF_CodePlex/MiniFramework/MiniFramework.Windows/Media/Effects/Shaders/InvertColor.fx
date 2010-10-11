// <copyright file="InvertColor.fx" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Shader de la classe <c>MiniFramework.Windows.Media.Effects.InvertColorEffect</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

//// ========================================================================================

// Entrée de surface
sampler2D Input: register(S0);

// Point d'entrée du programme
float4 main(float2 Point: TEXCOORD): COLOR
{
	float4 Color=tex2D(Input, Point);
	float4 InvertedColor=1-Color;

	InvertedColor.a=Color.a;
	InvertedColor.rgb*=InvertedColor.a;
	return InvertedColor;
}