<?xml version="1.0"?>

<!--
//  Jim Awe
//  Autodesk, Inc.
-->

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:output method="html"/>

    <xsl:template match="/">
        <html>
            <head>
            	<link rel='stylesheet' type='text/css' href='../css/Reports.css'/>
            </head>
            <body>
                <xsl:apply-templates select="//Drawing" />
            </body>
        </html>
    </xsl:template>


    <xsl:template match="Drawing">
        <hr/>
        <table>
            <tr class="drawing">
                <td class="label">Drawing:</td><td width="1000"><xsl:value-of select="@path"/></td>
            </tr>
        </table>
        <hr/>

        <xsl:apply-templates select="Symbols" />
    </xsl:template>



    <xsl:template match="Symbols">
        <xsl:apply-templates select="References" />
    </xsl:template>



    <xsl:template match="References">
        <table class="small" border="3" cellpadding="5">
            <caption align="left">References To Symbols</caption>
            <thead>
                <tr>
                    <th>Class</th>
                    <th>Name</th>
                    <th>References</th>
                </tr>
            </thead>

            <tbody>
                <xsl:apply-templates select="ObjectType" />
            </tbody>
        </table>
        <br/>

    </xsl:template>


    <xsl:template match="ObjectType">
        <xsl:variable name="x" select="@class" />

        <tr class="styleheader">
            <td colspan="3"><xsl:value-of select="@class"/></td>
        </tr>
            

        <xsl:for-each select="Symbol">
            <xsl:sort select="@references" data-type="number" order="descending"/>
            <tr>
                    <!-- set the background color of the row based on the object type -->
                <xsl:attribute name="class">
                    <xsl:choose>
                        <xsl:when test="starts-with($x, 'Aec')">aecobject</xsl:when>
                        <xsl:otherwise>nonaecobject</xsl:otherwise>
                    </xsl:choose>
                </xsl:attribute>

                <td></td>
                <td><xsl:value-of select="@name"/></td>
                <td align="right"><xsl:value-of select="@references"/></td>
            </tr>
        </xsl:for-each>
    </xsl:template>

</xsl:stylesheet>
