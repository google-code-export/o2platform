// <copyright file="DirectionalBlur.fx" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Shader de la classe <c>MiniFramework.Windows.Media.Effects.DirectionalBlurEffect</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

//// ========================================================================================

// Constantes
float Amount: register(C0);
float Direction: register(C1);

// Entrée de surface
sampler2D Input: register(S0);

// Point d'entrée du programme
float4 main(float2 Point: TEXCOORD): COLOR
{
	float4 Color=0;
	float Angle=Direction*0.0174533f;
	float xOffset=cos(Angle);
	float yOffset=sin(Angle);

	for(int i=0; i<16; i++)
	{
		Point.x=Point.x-Amount*xOffset;
		Point.y=Point.y-Amount*yOffset;
		Color+=tex2D(Input, Point);
	}

	Color/=16;
	return Color;
}