<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
    <xsl:param name="project-name" select="'Single Source of Truth - SSoT.me'" />

    <xsl:param name="output-filename" select="'output.txt'" />

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
            <FileSetFiles>
                <FileSetFile>
                    <RelativePath>
                        <xsl:text>index.md</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve"># <xsl:value-of select="$project-name"/> Project Documentation

This documentation provides background on these open source tools.


# SSoT.me Schema
The SSoT.me infrastructure is all based on the following (relatively) [simple schema](./schema/SinglePageDocs.html).


# Helpful Links

[Github Repo](https://github.com/SSoTme/SSoTme-Open-Source-Tools)
[Github Pages Docs](https://ssotme.github.io/SSoTme-Open-Source-Tools/)
[SSoT.me Schema](https://ssotme.github.io/SSoTme-Open-Source-Tools/schema/SinglePageDocs.html)

# Contact
Contact EJ Alexandra for further details.
</xsl:element>
                </FileSetFile>
            </FileSetFiles>
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
