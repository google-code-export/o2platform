<?xml version="1.0" encoding="iso-8859-1" ?>
<!-- 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="iso-8859-1" />

  <xsl:template match="/">
    <Assembly>
      <xsl:copy-of select="Assembly/@*"/>
      <xsl:apply-templates />
    </Assembly>
  </xsl:template>

  <xsl:template match="*">
    <xsl:copy>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates />
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>
