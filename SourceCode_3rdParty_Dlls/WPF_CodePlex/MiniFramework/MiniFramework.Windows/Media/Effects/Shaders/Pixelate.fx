// <copyright file="Pixelate.fx" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Shader de la classe <c>MiniFramework.Windows.Media.Effects.PixelateEffect</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

//// ========================================================================================

// Constantes
float BlockHeight: register(C0);
float BlockWidth: register(C1);

// Entrée de surface
sampler2D Input: register(S0);

// Point d'entrée du programme
float4 main(float2 Point: TEXCOORD): COLOR
{
	float2 BlockCount={ BlockWidth, BlockHeight };
	float2 BlockSize=1.0/BlockCount;

	float2 OffsetPoint=Point;
	if(floor(OffsetPoint.y/BlockSize.y)%2.0>=1.0) OffsetPoint.x+=BlockSize.x/2.0;

	float2 BlockNumber=floor(OffsetPoint/BlockSize);
	return tex2D(Input, (BlockNumber*BlockSize)+(BlockSize/2));
}