function registerEvents()
{
    document.getElementById("parsePlateButton").addEventListener("click", function(){
        parsePlate();
    });
}

function parsePlate(plate = "", country = "")
{
    console.log(plate);

    if(plate == "")
    {
        plate = document.getElementById("parsePlateInput").value;

        if(plate == "")
        {
            document.getElementById("results").classList.remove("visible");
        }
    }

    if(country != "")
    {
        plate = `${country}/${plate}`;
    }

    fetch(`/api/plate/${plate}`, {
        method: 'get'
    })
    .then(response => response.json())
    .then(data => renderOutput(data));
}

function renderOutput(data)
{
    if(data.plates.length == 1)
    {
        renderPlateDetails(data.plates[0]);
    }
    else if(data.plates.length == 0)
    {
        renderNoMatches();
    }
    else
    {
        renderMultipleMatches(data.plates);
    }

    document.getElementById("results").classList.add("visible");

    console.log(data);
}

function renderNoMatches()
{
    document.getElementById("resultItems").innerHTML = `<div class="no-results-header">No matches found</div>
<div class="no-results-text">
    The country may not be supported, or you're entering a local private/custom plate.
</div>`;
}

function renderMultipleMatches(plates)
{
    document.getElementById("resultItems").innerHTML = `<div class="multiple-results-header">${plates.length} matches found</div>`;

    plates.forEach(function(plate){
        addMultipleMatchItem(plate);
    });
}

function renderPlateDetails(plate)
{
    var info = "";

    document.getElementById("resultItems").innerHTML = "";

    addCountryItem(plate);

    switch(plate.country.code)
    {
        case "al":
            switch(plate.info.formatEnum)
            {
                case 42:
                    break;
                case 43:
                    addDetailItem("📍", "Region", plate.info.region);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
                case 44:
                    addDetailItem("🏢", "Diplomatic Org.", plate.info.diplomatic.organisation);
                    info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                    break;
            }
            break;

        case "at":
            switch(plate.info.formatEnum)
            {
                case 6:
                    addDetailItem("📍", "Registration Office", plate.info.region);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;

        case "de":
            switch(plate.info.formatEnum)
            {
                case 7:
                    addDetailItem("📍", "Region", plate.info.region);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;

        case "fi":
            switch(plate.info.formatEnum)
            {
                case 46:
                    addDetailItem("📍", "Region", plate.info.region);
                    addDetailItem("📑", "Issue", `${plate.info.issue} [${plate.info.series}]`);
                    break;
                case 47:
                    addDetailItem("📑", "Issue", `${plate.info.issue} [${plate.info.series}]`);
                    addDetailItem("🚘", "Vehicle Type", plate.info.vehicleType);
                    break;
                case 48:
                    addDetailItem("📑", "Issue", `${plate.info.issue} [${plate.info.series}]`);
                    info = "Temporary plate used for vehicles exported from Finland.";
                    break;
                case 49:
                    info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                    break;
            }

        case "fr":
            switch(plate.info.formatEnum)
            {
                case 10:
                    addDetailItem("📍", "Registration Department", plate.info.region);
                    break;
                case 11:
                    addDetailItem("📑", "Issue No.", plate.info.issue);
                    addDetailItem("📅", "Year", plate.info.registrationYear);
                    info = "<strong>Year</strong> is approximate, based on a steady average amount of cars per year, providing the SIV system lasts the estimated 80 years initially planned.";
                    break;
            }
            break;

        case "gb":
            switch(plate.info.formatEnum)
            {
                case 25:
                case 26:
                case 27:
                    addDetailItem("📍", "DVLA Office", plate.info.region);
                    addDetailItem("📑", "Issue No.", plate.info.issue);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
                case 28:
                case 29:
                case 30:
                    addDetailItem("📍", "DVLA Office", plate.info.region);
                    addDetailItem("📅", "Year", plate.info.registrationYear);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
                case 31:
                    addDetailItem("📑", "Issue No.", plate.info.issue);
                    info = "Licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                    break;
                case 32:
                    addDetailItem("🏢", "Diplomatic Org.", plate.info.diplomatic.organisation);
                    addDetailItem("🗺️", "Diplomatic Type", plate.info.diplomatic.type);
                    addDetailItem("🛡️", "Diplomatic Rank", plate.info.diplomatic.rank);
                    info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                    break;
            }
            break;

        case "gb-nir":
            switch(plate.info.formatEnum)
            {
                case 41:
                    addDetailItem("📍", "Region", plate.info.region);
                    addDetailItem("📑", "Issue No.", plate.info.issue);
                    addDetailItem("🌟", "Special", plate.info.special);
                    info = "Northern Ireland, although part of Great Britain, has its own plate format.";
                    break;
            }
            break;

        case "gg":
            switch(plate.info.formatEnum)
            {
                case 5:
                    addDetailItem("📑", "Issue No.", plate.info.issue);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;

        case "hu":
            switch(plate.info.formatEnum)
            {
                case 50:
                    addDetailItem("📅", "Registration Year", plate.info.registrationYear);
                    break;
            }
            break;

        case "it":
            switch(plate.info.formatEnum)
            {
                case 45:
                    addDetailItem("📍", "Region", plate.info.region);
                    break;
            }
            break;

        case "jp":
            switch(plate.info.formatEnum)
            {
                case 34:
                    info = "Issued to Japanese citizens for internationl travel &mdash; the Japanese writing system is considered unacceptable outside of Japan, as they are not easily identifiable to local authorities."
                case 33:
                    addDetailItem("📍", "Region", plate.info.region);
                    addDetailItem("🚘", "Vehicle Type", plate.info.vehicleType);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;

        case "lt":
            switch(plate.info.formatEnum)
            {
                case 35:
                    addDetailItem("📍", "Region", plate.info.region);
                    info = "<strong>Region</strong> is only valid for vehicles registered between 1991 and 2004."
                    break;
                case 36:
                    info = "Temporary plate used for vehicles imported and exported to/from Lithuania, only valid for 90 days.";
                    break;
                case 37:
                    info = "Licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                    break;
                case 38:
                    addDetailItem("🏢", "Diplomatic Org.", plate.info.diplomatic.organisation);
                    info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                    break;
                case 39:
                    info = "Found on taxis and private-hire vehicles.";
                    break;
                case 40:
                    info = "Found on vehicles used in the military for transport on public roads";
                    break;
            }
            break;

        case "lv":
            switch(plate.info.formatEnum)
            {
                case 51:
                    if(plate.info.series == null)
                    {
                        addDetailItem("📑", "Issue", `${plate.info.issue}`)
                    }
                    else
                    {
                        addDetailItem("📑", "Issue", `${plate.info.issue} [${plate.info.series}]`)
                    }
                    addDetailItem("🚘", "Vehicle Type", plate.info.vehicleType);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
                case 52:
                    info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                    break;
            }
            break;

        case "nl":
            switch(plate.info.formatEnum)
            {
                case 12:
                    addDetailItem("📍", "Region", plate.info.region);
                    break;
                default:
                    addDetailItem("📅", "Year", plate.info.registrationYear);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;

        case "ru":
            switch(plate.info.formatEnum)
            {
                case 4:
                    addDetailItem("📍", "Registration Office", plate.info.region);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;
    }

    addDetailItem("🔢", "Format", plate.info.format);

    if(info != "")
    {
        addTextItem(info);
    }
}

function addCountryItem(plate)
{
    addDetailItem(plate.country.flag, `${plate.country.letter} &bull; ${plate.country.name}`, plate.parsed)
}

function addMultipleMatchItem(plate)
{
    var itemHtml = `<a href="javascript:void(0);" class="multiple-results-item" onclick="parsePlate('${plate.parsed}', '${plate.country.code}')">
        ${plate.country.flag} ${plate.country.name}
</a>`;

    document.getElementById("resultItems").innerHTML += itemHtml;
}

function addDetailItem(icon, item, content)
{
    if(content == "No")
    {
        content = "<em>Standard</em>";
    }

    if(content == "Unknown")
    {
        content = "<em>Unknown</em>";
    }

    if(content != null)
    {
        content = content
            .toString()
            .replace("(", "<em>(")
            .replace(")", ")</em>")
            .replace("[", "(")
            .replace("]", ")");
    }

    var itemHtml = `<div class="results-item">
    <div class="results-item-icon vertical-align">
        <div class="vertical-align-inner">${icon}</div>
    </div>
    <div class="results-item-title">
        ${item}
    </div>
    <div class="results-item-content">
        ${content}
    </div>
</div>`;

    document.getElementById("resultItems").innerHTML += itemHtml;
}

function addTextItem(content)
{
    var itemHtml = `<div class="results-text-item">
    ${content}
</div>`;

    document.getElementById("resultItems").innerHTML += itemHtml;
}

registerEvents();