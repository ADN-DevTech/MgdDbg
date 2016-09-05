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

        <xsl:apply-templates select="Entities" />
    </xsl:template>



    <xsl:template match="Entities">
        <xsl:apply-templates select="RawCount" />
    </xsl:template>



    <xsl:template match="RawCount">
        <table class="small" border="3" cellpadding="5">
            <caption>Raw Count</caption>
            <thead>
                <tr>
                    <th>Class</th>
                    <th>Display Name</th>
                    <th>Count</th>
                </tr>
            </thead>
            <tbody>
                <xsl:for-each select="ObjectType">
                    <xsl:sort select="@count" data-type="number" order="descending"/>
                    <tr>
                            <!-- set the background color of the row based on the object type -->
                        <xsl:attribute name="class">
                            <xsl:variable name="x" select="@class" />
                            <xsl:choose>
                                <xsl:when test="starts-with($x, 'Aec')">aecobject</xsl:when>
                                <xsl:otherwise>nonaecobject</xsl:otherwise>
                            </xsl:choose>
                        </xsl:attribute>

                        <td><xsl:value-of select="@class"/></td>
                        <td><xsl:value-of select="@displayName"/></td>
                        <td align="right"><xsl:value-of select="@count"/></td>
                    </tr>
                </xsl:for-each>
            </tbody>
        </table>
        <br/>
    </xsl:template>

    <xsl:template match="ObjectType">
        <tr>
            <td><xsl:value-of select="@class"/></td>
            <td><xsl:value-of select="@count"/></td>
        </tr>
    </xsl:template>

</xsl:stylesheet>
