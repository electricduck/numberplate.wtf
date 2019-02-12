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
</div>
<!--<div class="no-results-text sm">
    <div class="country-buttons">
        <span class="button">🇦🇹 Austria*</span>
        <span class="button">🇫🇷 France*</span>
        <span class="button">🇩🇪 Germany</span>
        <span class="button">🇬🇬 Guernsey</span>
        <span class="button">🇳🇱 Netherlands*</span>
        <span class="button">🇳🇴 Norway</span>
        <span class="button">🇷🇺 Russia</span>
        <span class="button">🇪🇸 Spain</span>
        <span class="button">🇬🇧 United Kingdom</span>
    </div>
</div>
<div class="no-results-text xs">
    <strong>*</strong> Not all formats supported
</div>-->`;
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
    var countryName = getCountryName(plate.country.code);
    addDetailItem(plate.country.flag, countryName, plate.parsed)
}

function getCountryName(code)
{
    switch(code)
    {
        case "at":
            return "Austria";
            break;
        case "de":
            return "Germany";
            break;
        case "es":
            return "Spain";
            break;
        case "fr":
            return "France";
            break;
        case "gb":
            return "United Kingdom";
            break;
        case "gg":
            return "Guernsey";
            break;
        case "nl":
            return "Netherlands";
            break;
        case "no":
            return "Norway";
            break;
        case "ru":
            return "Russia";
            break;
    }

    return "Unknown";
}

function addMultipleMatchItem(plate)
{
    var itemHtml = `<a href="javascript:void(0);" class="multiple-results-item" onclick="parsePlate('${plate.parsed}', '${plate.country.code}')">
        ${plate.country.flag} ${getCountryName(plate.country.code)}
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