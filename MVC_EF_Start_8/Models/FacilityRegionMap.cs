using System.Collections.Generic;

namespace MVC_EF_Start_8.Models
{
    public static class FacilityRegionMap
    {
        public static readonly Dictionary<string, string> Regions = new Dictionary<string, string>
        {
            // Southeast
            ["Vogtle"] = "Southeast",
            ["McGuire Nuclear Station"] = "Southeast",
            ["Turkey Point"] = "Southeast",
            ["Browns Ferry Nuclear Plant"] = "Southeast",
            ["Catawba Nuclear Station"] = "Southeast",
            ["St. Lucie Nuclear Power Plant"] = "Southeast",
            ["Virgil C. Summer Nuclear Station"] = "Southeast",
            ["Watts Bar Nuclear Plant"] = "Southeast",
            ["Joseph M. Farley Nuclear Plant"] = "Southeast",
            ["Oconee Nuclear Station"] = "Southeast",
            ["North Anna Nuclear Generating Station"] = "Southeast",
            ["Surry Power Station"] = "Southeast",
            ["Sequoyah Nuclear Plant"] = "Southeast",
            ["Crystal River Nuclear Plant"] = "Southeast",
            ["Harris Nuclear Plant"] = "Southeast",
            ["Shearon Harris"] = "Southeast",

            // Midwest
            ["Braidwood Generating Station"] = "Midwest",
            ["Byron Generating Station"] = "Midwest",
            ["Quad Cities Generating Station"] = "Midwest",
            ["Prairie Island Nuclear Generating Plant"] = "Midwest",
            ["Point Beach Nuclear Plant"] = "Midwest",
            ["Cooper Nuclear Station"] = "Midwest",
            ["Davis Besse Nuclear Power Station"] = "Midwest",
            ["Duane Arnold Energy Center"] = "Midwest",
            ["LaSalle County Generating Station"] = "Midwest",
            ["Clinton Power Station"] = "Midwest",
            ["Monticello Nuclear Generating Plant"] = "Midwest",
            ["Callaway Plant"] = "Midwest",
            ["Palisades Nuclear Generating Station"] = "Midwest",
            ["Enrico Fermi Nuclear Generating Station"] = "Midwest",
            ["Donald C. Cook Nuclear Plant"] = "Midwest",

            // Northeast
            ["Limerick Generating Station"] = "Northeast",
            ["Seabrook Station"] = "Northeast",
            ["PSEG Hope Creek Generating Station"] = "Northeast",
            ["Indian Point Energy Center"] = "Northeast",
            ["Millstone Power Station"] = "Northeast",
            ["Nine Mile Point Nuclear Station"] = "Northeast",
            ["James A. FitzPatrick Nuclear Power Plant"] = "Northeast",
            ["Peach Bottom Atomic Power Station"] = "Northeast",
            ["Calvert Cliffs Nuclear Power Plant"] = "Northeast",
            ["Susquehanna Steam Electric Station"] = "Northeast",
            ["Oyster Creek Nuclear Generating Station"] = "Northeast",
            ["Pilgrim Nuclear Power Station"] = "Northeast",
            ["Vermont Yankee Nuclear Power Plant"] = "Northeast",
            ["Three Mile Island Nuclear Generating Station"] = "Northeast",

            // South Central
            ["Comanche Peak Nuclear Power Plant"] = "South Central",
            ["Arkansas Nuclear One"] = "South Central",
            ["South Texas Project"] = "South Central",
            ["Grand Gulf Nuclear Station"] = "South Central",
            ["River Bend Station"] = "South Central",
            ["Waterford Steam Electric Station"] = "South Central",

            // West
            ["Palo Verde Nuclear Generating Station"] = "West",
            ["Columbia Generating Station"] = "West",
            ["San Onofre Nuclear Generating Station"] = "West",
            ["Diablo Canyon Power Plant"] = "West",

            // Special cases
            ["Fort Calhoun Nuclear Generating Station"] = "Midwest",
            ["Kewaunee Power Station"] = "Midwest"
        };
    }
}
