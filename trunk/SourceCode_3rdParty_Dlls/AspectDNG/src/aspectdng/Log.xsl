<?xml version="1.0" encoding="iso-8859-1"?>
<!-- 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
   <xsl:output method="html" encoding="iso-8859-1" />

   <xsl:template match="AspectDngLog">
      <html>
         <head>
<style>
.origin{
    background-color: #aaeeff;
}
div{
    display: none;
    position: absolute;
    background-color: #aaeebb;
    color: black;
}
a:hover div{
    display: block;
}
</style>
         </head>

         <body>
            <table width="100%" border="1">
               <tr>
                  <th width="10%">Meta-aspect</th>
                  <th width="5%"># <br/>targets</th>
                  <th width="40%">Targets</th>
                  <th width="5%"># <br/>aspects</th>
                  <th width="40%">Aspects</th>
               </tr>

               <xsl:apply-templates />
            </table>
         </body>
      </html>
   </xsl:template>

   <xsl:template match="*">
      <xsl:variable name="previousOrigin" select="preceding-sibling::*[1]/Origin" />

      <xsl:if test="not($previousOrigin) or Origin != $previousOrigin">
         <tr class="origin">
            <td colspan="5">[From <xsl:value-of select="Origin" />]</td>
         </tr>
      </xsl:if>

      <tr>
		<td><xsl:value-of select="name()" />	</td>
		
		<td><xsl:value-of select="NbTargets" /></td>
         <td>
            <a href="#">
               <xsl:value-of select="TargetRegExp | TargetXPath" />
               <div> <xsl:value-of select="RealTargetXPath" /> </div>
            </a>
         </td>

		<td><xsl:value-of select="NbAspects" /></td>
         <td>
            <a href="#">
               <xsl:value-of select="AspectRegExp | AspectXPath" />
               <div><xsl:value-of select="RealAspectXPath" /></div>
            </a>
         </td>
      </tr>
   </xsl:template>
</xsl:stylesheet>

