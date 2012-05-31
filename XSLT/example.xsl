<?xml version="1.0" encoding="ISO-8859-1"?>

<xsl:stylesheet version="1.1" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- Main template -->
  <xsl:template match="/">

    <html>
      <header>
        <title>
          <!-- Get title from Options -->
          <xsl:call-template name="GetTranslation" >
            <xsl:with-param name="id" select="'PAGE_TITLE'" />
          </xsl:call-template>
        </title>
      </header>
      <body>

        <!-- Check if title is shown on page -->
        <xsl:if test="//Root/Options/row[@ShowTitle = 1]">
          <h1>
            <xsl:call-template name="GetTranslation" >
              <xsl:with-param name="id" select="'PAGE_TITLE'" />
            </xsl:call-template>
          </h1>
        </xsl:if>

        <!-- Match all items automatically to correct template -->
        <h5>
          <xsl:call-template name="GetTranslation" >
            <xsl:with-param name="id" select="'APPLY_TEMPLATE_ITEMS'" />
          </xsl:call-template>
        </h5>
        <xsl:apply-templates select="//Root/Items" />

        <!-- Call template for all items -->
        <h5>
          <xsl:call-template name="GetTranslation" >
            <xsl:with-param name="id" select="'CALL_TEMPLATE_ITEMS'" />
          </xsl:call-template>
        </h5>
        <xsl:call-template name="ProcessAllItems">
          <xsl:with-param name="items" select="//Root/Items" />
        </xsl:call-template>

        <h5>
          No Template without mode, so will just print all child elements
        </h5>
        <xsl:apply-templates select="//Root/Users" />

        <h5>
          Print all user info
        </h5>
        <xsl:apply-templates select="//Root/Users" mode="AllInfo"/>

        <h5>
          Print only user name
        </h5>
        <xsl:apply-templates select="//Root/Users" mode="OnlyName"/>
      </body>
    </html>

  </xsl:template>

  <!-- Get Translation-->
  <xsl:template name="GetTranslation">
    <xsl:param name="id" />
    <xsl:param name="replace" />

    <!-- Check if tranlation exists and insert value-->
    <xsl:choose>
      <xsl:when test="count(//Root/Translations/Translation[DictionaryID=$id]) &gt; 0">
        <xsl:variable name="text">
          <xsl:value-of select="//Root/Translations/Translation[DictionaryID=$id]/Text"/>
        </xsl:variable>

        <xsl:if test="$replace != ''">
          <xsl:if test="contains($text, '{0}')">
            <!-- Implement text replace -->
          </xsl:if>
        </xsl:if>

        <xsl:value-of select="$text"/>

      </xsl:when>
      <xsl:otherwise>
        Missing:&#160;<xsl:value-of select="$id" />
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>

  <!-- Loop all items and print position and id -->
  <xsl:template name="ProcessAllItems">
    <xsl:param name="items" />

    <xsl:for-each select="$items/*">
      <xsl:variable name="rowId">
        <xsl:value-of select="position()" />
      </xsl:variable>

      Row: <xsl:value-of select="$rowId" />
      ItemID: <xsl:value-of select="current()/@ID" />
      Price: <xsl:value-of select="current()/Price" /> <br/>
      <xsl:value-of select="current()/Description" /> <br/>
    </xsl:for-each>
  </xsl:template>

  <!-- Template matches based on Item type attribute -->
  <xsl:template match="Item[@type=1]">
    ItemID: <xsl:value-of select="current()/@ID" /> is Type 1 <br/>
  </xsl:template>

  <xsl:template match="Item[@type=2]">
    ItemID: <xsl:value-of select="current()/@ID" /> is Type 2 <br/>
  </xsl:template>

  <!-- This will be used from Item type 1, because its last specific for type 1-->
  <xsl:template match="Item[@type=1]">
    Second template for Item type 1. <br/>
  </xsl:template>

  <!-- Items with type 1 and 2 will be matched with more specific ones -->
  <xsl:template match="Item">
    ItemID: <xsl:value-of select="current()/@ID" /> is Type <xsl:value-of select="current()/@type" /> (From general item template)<br/>
  </xsl:template>

  <!-- Template matches based on Item type attribute -->
  <xsl:template match="User" mode="OnlyName">
    Name: <xsl:value-of select="current()/FirstName" /> <xsl:value-of select="current()/LastName" /><br/>
  </xsl:template>

  <xsl:template match="User" mode="AllInfo">
    ID: <xsl:value-of select="current()/@ID" /><br/>
    Name: <xsl:value-of select="current()/FirstName" /> <xsl:value-of select="current()/LastName" /><br/>
    Title: <xsl:value-of select="current()/Title[@lan='EN']" /><br/>
  </xsl:template>

</xsl:stylesheet>