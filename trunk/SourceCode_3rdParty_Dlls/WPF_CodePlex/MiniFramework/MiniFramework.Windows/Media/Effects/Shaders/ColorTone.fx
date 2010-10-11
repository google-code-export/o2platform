// <copyright file="ColorTone.fx" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Shader de la classe <c>MiniFramework.Windows.Media.Effects.ColorToneEffect</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

//// ========================================================================================

// Constantes
float Amount: register(C0);
float4 DarkColor: register(C1);
float Desaturation: register(C2);
float4 LightColor: register(C3);

// Entrée de surface
sampler2D Input: register(S0);

// Point d'entrée du programme
float4 main(float2 Point: TEXCOORD): COLOR
{
	float4 Color=LightColor*tex2D(Input, Point);
	float Gray=dot(float3(0.3, 0.59, 0.11), Color.rgb);

	float3 Muted=lerp(Color.rgb, Gray.xxx, Desaturation);
	float3 Middle=lerp(DarkColor, LightColor, Gray);

	Color.rgb=lerp(Muted, Middle, Amount);
	return Color;
}