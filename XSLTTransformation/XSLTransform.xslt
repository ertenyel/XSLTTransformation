<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>
	<xsl:key name="groupKey" match="list/item/@group" use="." />
	<xsl:template match="list">
		<xsl:element name="groups">
			<xsl:for-each select="item/@group[generate-id() = generate-id(key('groupKey',.)[1])]">
				<xsl:variable name ="i" select ="."/>
				<xsl:element name="group">
					<xsl:attribute name="name">
						<xsl:value-of select="$i"/>
					</xsl:attribute>
					<xsl:for-each select ="//item[@group = $i]/@name">
						<xsl:element name ="item">
							<xsl:attribute name="name">
								<xsl:value-of select="."/>
							</xsl:attribute>
						</xsl:element>
					</xsl:for-each>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>