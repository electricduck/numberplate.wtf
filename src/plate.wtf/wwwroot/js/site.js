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
        case "at":
            switch(plate.info.formatEnum)
            {
                case 6:
                    addDetailItem("📍", "Registration Office", plate.info.region);
                    addDetailItem("🌟", "Special", plate.info.special);
                    break;
            }
            break;

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
                    info = "This is a trade plate, licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                    break;
                case 32:
                    addDetailItem("🏢", "Diplomatic Org.", plate.info.diplomatic.organisation);
                    addDetailItem("🗺️", "Diplomatic Type", plate.info.diplomatic.type);
                    addDetailItem("🛡️", "Diplomatic Rank", plate.info.diplomatic.rank);
                    info = "This is a diplomatic plate, found on cars used by foreign embassies, high commissions, consulates and international organisations. The cars themselves are usually not personally owned.";
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

        case "jp":
            switch(plate.info.formatEnum)
            {
                case 34:
                    info = "This is an out-of-country plate, issued to Japanese citizens for internationl travel &mdash; the Japanese writing system is considered unacceptable outside of Japan, as they are not easily identifiable to local authorities."
                case 33:
                    addDetailItem("📍", "Region", plate.info.region);
                    addDetailItem("🚘", "Vehicle Type", plate.info.vehicleType);
                    addDetailItem("🌟", "Special", plate.info.special);
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
    addDetailItem(plate.country.flag, `${plate.country.letter} &bull; ${plate.country.name}`, plate.country.name)
}

function getCountryName(code)
{
    switch(code)
    {
        case "at":
            return "Austria";
        case "de":
            return "Germany";
        case "es":
            return "Spain";
        case "fr":
            return "France";
        case "gb":
            return "United Kingdom";
        case "gg":
            return "Guernsey";
        case "jp":
            return "Japan";
        case "nl":
            return "Netherlands";
        case "no":
            return "Norway";
        case "ru":
            return "Russia";
    }

    return "Unknown";
}

function addMultipleMatchItem(plate)
{
    var itemHtml = `<a href="javascript:void(0);" class="multiple-results-item" onclick="parsePlate('${plate.parsed}', '${plate.country.code}')">
        ${plate.country.flag} ${plate.country.code}
</a>`;

    document.getElementById("resultItems").innerHTML += itemHtml;
}

function addDetailItem(icon, item, content)
{
    if(content == null)
    {
        if(item == "Special")
        {
            content = "<em>Standard</em>";
        }
        else
        {
            content = "<em>Unknown</em>";
        }
    }

    content = content
        .toString()
        .replace("(", "<em>(")
        .replace(")", ")</em>");

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