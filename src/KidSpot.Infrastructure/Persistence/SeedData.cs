using KidSpot.Domain.Entities;
using KidSpot.Domain.ValueObjects;

namespace KidSpot.Infrastructure.Persistence;

public static class SeedData
{
    private static readonly Guid AdminId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    public static List<Place> GetPlaces()
    {
        var places = new List<Place>();
        places.AddRange(Praha());
        places.AddRange(Bratislava());
        places.AddRange(Wien());
        places.AddRange(Berlin());
        places.AddRange(Paris());
        places.AddRange(London());
        places.AddRange(Roma());
        places.AddRange(Madrid());
        places.AddRange(Amsterdam());
        places.AddRange(Warszawa());
        places.AddRange(Budapest());
        places.AddRange(Lisboa());
        places.AddRange(Bruxelles());
        places.AddRange(Stockholm());
        places.AddRange(Kobenhavn());
        places.AddRange(Helsinki());
        places.AddRange(Oslo());
        places.AddRange(Zagreb());
        places.AddRange(Ljubljana());
        places.AddRange(Athinai());
        return places;
    }

    // ────────────── Praha ──────────────
    private static List<Place> Praha() => new()
    {
        Place.Create("Dětské hřiště Kampa",
            "Oblíbené hřiště na Kampě s výhledem na Vltavu. Nové herní prvky, dopadová plocha, oplocené.",
            new Location(50.0835, 14.4069, "Na Kampě", "Praha", "Česko"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Restaurace Lokál Dlouhááá",
            "Rodinně přátelská restaurace s tradiční českou kuchyní. Dětský koutek a dětské menu.",
            new Location(50.0903, 14.4258, "Dlouhá 33", "Praha", "Česko"),
            PlaceType.Restaurant, new KidsAttributes(true, true, true, 0, 12), AdminId),
        Place.Create("Národní technické muzeum",
            "Interaktivní expozice pro děti. Doprava, astronomie, důlní štola.",
            new Location(50.0969, 14.4253, "Kostelní 42", "Praha", "Česko"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 4, 15), AdminId),
        Place.Create("Zoo Praha",
            "Jedna z nejlepších zoo na světě. Dětská zoo, lanový park, vláček.",
            new Location(50.1167, 14.4064, "U Trojského zámku 3/120", "Praha", "Česko"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Petřínská rozhledna",
            "Výstup na mini Eiffelovku. Zrcadlové bludiště hned vedle.",
            new Location(50.0836, 14.3953, "Petřínské sady", "Praha", "Česko"),
            PlaceType.Other, new KidsAttributes(false, false, false, 3, 15), AdminId),
        Place.Create("Stromovka",
            "Obrovský park s hřišti, inline dráhou a planetáriem.",
            new Location(50.1058, 14.4186, "Královská obora", "Praha", "Česko"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
        Place.Create("Aquapalace Praha",
            "Největší aquapark ve střední Evropě. Dětský svět, tobogány, divoká řeka.",
            new Location(50.0022, 14.4906, "Pražská 138", "Praha", "Česko"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Kavárna Místo",
            "Klidná kavárna s herním koutkem. Přebalovací pult, ohřev lahví.",
            new Location(50.0762, 14.4189, "Lublaňská 18", "Praha", "Česko"),
            PlaceType.Restaurant, new KidsAttributes(true, true, true, 0, 6), AdminId),
        Place.Create("Letenské sady — hřiště",
            "Velké moderní hřiště s trampolínami a lezeckými prvky.",
            new Location(50.0972, 14.4194, "Letenské sady", "Praha", "Česko"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 2, 12), AdminId),
        Place.Create("SEA LIFE Praha",
            "Mořské akvárium v Holešovicích. Žraloci, korálový tunel, dotykový bazén.",
            new Location(50.1053, 14.4322, "Výstaviště 422", "Praha", "Česko"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 2, 14), AdminId),
    };

    // ────────────── Bratislava ──────────────
    private static List<Place> Bratislava() => new()
    {
        Place.Create("Bibiana — Medzinárodný dom umenia pre deti",
            "Interaktivní galerie a dílny pro děti všech věkových kategorií.",
            new Location(48.1410, 17.1077, "Panská 41", "Bratislava", "Slovensko"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 2, 12), AdminId),
        Place.Create("DinoPark Bratislava",
            "Životní modely dinosaurů v přírodním prostředí. 3D kino a hřiště.",
            new Location(48.1695, 17.0722, "Lamač", "Bratislava", "Slovensko"),
            PlaceType.Park, new KidsAttributes(true, true, true, 2, 14), AdminId),
        Place.Create("Detské ihrisko Sad Janka Kráľa",
            "Největší městský park v Bratislavě s moderním hřištěm u Dunaje.",
            new Location(48.1358, 17.1125, "Sad Janka Kráľa", "Bratislava", "Slovensko"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Zoo Bratislava",
            "Menší zoo s domácími zvířaty a exotickými druhy. Dětský koutek.",
            new Location(48.1759, 17.0735, "Mlynská dolina 1A", "Bratislava", "Slovensko"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 15), AdminId),
        Place.Create("Aquapark Senec",
            "Oblíbený aquapark nedaleko Bratislavy. Tobogány a dětský bazén.",
            new Location(48.2197, 17.3986, "Slnečné jazerá", "Bratislava", "Slovensko"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
    };

    // ────────────── Vídeň ──────────────
    private static List<Place> Wien() => new()
    {
        Place.Create("Tiergarten Schönbrunn",
            "Nejstarší zoo na světě. Pandy, sloni, dětská farma a hřiště.",
            new Location(48.1825, 16.3013, "Maxingstraße 13b", "Wien", "Österreich"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Prater — Wurstelprater",
            "Legendární zábavní park s ruským kolem. Atrakce pro všechny věky.",
            new Location(48.2166, 16.3964, "Prater", "Wien", "Österreich"),
            PlaceType.Other, new KidsAttributes(true, true, true, 2, 18), AdminId),
        Place.Create("ZOOM Kindermuseum",
            "Praktické muzeum pro děti. Workshopy, ateliéry, animace.",
            new Location(48.2033, 16.3581, "Museumsplatz 1", "Wien", "Österreich"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 0, 14), AdminId),
        Place.Create("Wasserspielplatz Donauinsel",
            "Vodní hřiště na Dunajském ostrově. Brodítka, fontány, pískoviště.",
            new Location(48.2280, 16.4085, "Donauinsel", "Wien", "Österreich"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Haus des Meeres",
            "Akvárium a terárium v protiletecké věži. Žraloci, opice, krokodýli.",
            new Location(48.1993, 16.3545, "Fritz-Grünbaum-Platz 1", "Wien", "Österreich"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 2, 15), AdminId),
        Place.Create("Familienrestaurant Luftburg",
            "Velká rodinná restaurace v Prateru s dětským koutkem a menu.",
            new Location(48.2153, 16.3984, "Prater 254", "Wien", "Österreich"),
            PlaceType.Restaurant, new KidsAttributes(true, true, true, 0, 12), AdminId),
    };

    // ────────────── Berlín ──────────────
    private static List<Place> Berlin() => new()
    {
        Place.Create("Zoologischer Garten Berlin",
            "Jedna z nejstarších a nejbohatších zoo v Evropě. Akvárium součástí.",
            new Location(52.5079, 13.3377, "Hardenbergplatz 8", "Berlin", "Deutschland"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("LEGOLAND Discovery Centre Berlin",
            "Interaktivní svět LEGO. 4D kino, stavební workshopy, jízdy.",
            new Location(52.5103, 13.3724, "Potsdamer Str. 4", "Berlin", "Deutschland"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 12), AdminId),
        Place.Create("Britzer Garten — Spielplatz",
            "Obrovský park s tematickými hřišti, vodními prvky a zahradami.",
            new Location(52.4313, 13.4017, "Sangerhauser Weg 1", "Berlin", "Deutschland"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 12), AdminId),
        Place.Create("Kinderbauernhof Görlitzer Park",
            "Dětská farma v Kreuzbergu. Poníci, kozy, králíci. Vstup zdarma.",
            new Location(52.4963, 13.4369, "Wiener Str. 59", "Berlin", "Deutschland"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 10), AdminId),
        Place.Create("Tropical Islands",
            "Obří tropický aquapark v bývalém hangáru. Bazény, pláže, tobogány.",
            new Location(52.0380, 13.7493, "Tropical-Islands-Allee 1", "Berlin", "Deutschland"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("MACHmit! Museum für Kinder",
            "Hands-on muzeum pro děti. Lezecká stěna, tiskárna, zrcadlový labyrint.",
            new Location(52.5398, 13.4108, "Senefelderstr. 5", "Berlin", "Deutschland"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 3, 12), AdminId),
    };

    // ────────────── Paříž ──────────────
    private static List<Place> Paris() => new()
    {
        Place.Create("Jardin du Luxembourg — Playground",
            "Historické hřiště v Lucemburské zahradě. Kolotoč, loutkové divadlo, ponycyklus.",
            new Location(48.8462, 2.3372, "Rue de Médicis", "Paris", "France"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Cité des Sciences — Cité des Enfants",
            "Největší vědecké muzeum ve Francii. Speciální sekce pro děti 2–12 let.",
            new Location(48.8958, 2.3869, "30 Avenue Corentin Cariou", "Paris", "France"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 2, 12), AdminId),
        Place.Create("Jardin d'Acclimatation",
            "Zábavní park v Bouloňském lesíku. Atrakce, zvířata, workshopy.",
            new Location(48.8783, 2.2615, "Bois de Boulogne", "Paris", "France"),
            PlaceType.Park, new KidsAttributes(true, true, true, 0, 14), AdminId),
        Place.Create("Aquarium de Paris",
            "Akvárium pod Trocadérem. Žraloci, medúzy, dotykový bazén.",
            new Location(48.8627, 2.2875, "5 Avenue Albert de Mun", "Paris", "France"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 2, 14), AdminId),
        Place.Create("Parc des Buttes-Chaumont",
            "Kopcovitý park s jeskyněmi, vodopádem a loutkářským divadlem.",
            new Location(48.8809, 2.3825, "1 Rue Botzaris", "Paris", "France"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
        Place.Create("Piscine Joséphine Baker",
            "Plovoucí bazén na Seině. Dětský bazén s výhledem na Notre-Dame.",
            new Location(48.8344, 2.3665, "Quai François Mauriac", "Paris", "France"),
            PlaceType.Pool, new KidsAttributes(true, false, true, 0, 18), AdminId),
    };

    // ────────────── Londýn ──────────────
    private static List<Place> London() => new()
    {
        Place.Create("Natural History Museum",
            "Dinosauři, příroda, vulkány. Vstup zdarma. Fantastické pro děti.",
            new Location(51.4967, -0.1764, "Cromwell Road", "London", "United Kingdom"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 18), AdminId),
        Place.Create("Diana Memorial Playground",
            "Pirátská loď, teepee, senzorická stezka v Kensington Gardens.",
            new Location(51.5091, -0.1873, "Kensington Gardens", "London", "United Kingdom"),
            PlaceType.Playground, new KidsAttributes(true, false, true, 0, 12), AdminId),
        Place.Create("London Zoo",
            "Historická zoo v Regent's Parku. Lvi, gorily, dětská farma.",
            new Location(51.5353, -0.1534, "Outer Circle, Regent's Park", "London", "United Kingdom"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Science Museum",
            "Interaktivní vědecké muzeum. Wonderlab, letadla, vesmír. Vstup zdarma.",
            new Location(51.4978, -0.1745, "Exhibition Road", "London", "United Kingdom"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 16), AdminId),
        Place.Create("Coram's Fields",
            "Park jen pro děti a jejich doprovod. Hřiště, zvířátka, kavárna.",
            new Location(51.5238, -0.1201, "93 Guilford St", "London", "United Kingdom"),
            PlaceType.Playground, new KidsAttributes(true, true, true, 0, 10), AdminId),
        Place.Create("Giraffe Restaurant — South Bank",
            "Rodinná restaurace u Temže. Dětské menu, omalovánky, vysoké židličky.",
            new Location(51.5055, -0.1165, "Riverside Level", "London", "United Kingdom"),
            PlaceType.Restaurant, new KidsAttributes(true, true, true, 0, 12), AdminId),
    };

    // ────────────── Řím ──────────────
    private static List<Place> Roma() => new()
    {
        Place.Create("Bioparco di Roma",
            "Zoo v parku Villa Borghese. Sloni, žirafy, lachtani.",
            new Location(41.9165, 12.4855, "Piazzale del Giardino Zoologico 1", "Roma", "Italia"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Explora — Museo dei Bambini",
            "Dětské hands-on muzeum. Supermarket, vodní stěna, staveniště.",
            new Location(41.9185, 12.4660, "Via Flaminia 82", "Roma", "Italia"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 0, 12), AdminId),
        Place.Create("Villa Borghese Playground",
            "Hřiště v nejkrásnějším římském parku. Půjčovna šlapadel u jezírka.",
            new Location(41.9142, 12.4858, "Villa Borghese", "Roma", "Italia"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Hydromania Waterpark",
            "Letní aquapark s tobogány a dětským světem. Otevřeno červen–září.",
            new Location(41.8328, 12.3578, "Vicolo del Casale Lumbroso 200", "Roma", "Italia"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 2, 18), AdminId),
        Place.Create("Parco degli Acquedotti",
            "Park s antickými akvadukty. Otevřený prostor pro běhání a piknik.",
            new Location(41.8525, 12.5558, "Via Lemonia", "Roma", "Italia"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
    };

    // ────────────── Madrid ──────────────
    private static List<Place> Madrid() => new()
    {
        Place.Create("Zoo Aquarium de Madrid",
            "Zoo, akvárium a delfinárium v jednom. Pandy, delfíni, tučňáci.",
            new Location(40.4104, -3.7685, "Casa de Campo", "Madrid", "España"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Parque de Atracciones de Madrid",
            "Zábavní park v Casa de Campo. Atrakce pro malé i velké.",
            new Location(40.4072, -3.7516, "Casa de Campo", "Madrid", "España"),
            PlaceType.Other, new KidsAttributes(true, true, true, 3, 18), AdminId),
        Place.Create("Parque del Retiro — Playground",
            "Hřiště v legendárním parku Retiro. Lodičky na jezírku, loutkové divadlo.",
            new Location(40.4153, -3.6845, "Plaza de la Independencia 7", "Madrid", "España"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 12), AdminId),
        Place.Create("Museo Nacional de Ciencias Naturales",
            "Dinosauři, minerály, interaktivní expozice. Skvělé pro zvídavé děti.",
            new Location(40.4404, -3.6893, "Calle de José Gutiérrez Abascal 2", "Madrid", "España"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 4, 16), AdminId),
        Place.Create("Faunia",
            "Přírodní park s ekosystémy od tropů po Antarktidu. Tučňáci a delfíni.",
            new Location(40.3858, -3.5915, "Av. de las Comunidades 28", "Madrid", "España"),
            PlaceType.Park, new KidsAttributes(true, true, true, 0, 18), AdminId),
    };

    // ────────────── Amsterdam ──────────────
    private static List<Place> Amsterdam() => new()
    {
        Place.Create("NEMO Science Museum",
            "Zelená budova ve tvaru lodi. 5 pater interaktivní vědy pro děti.",
            new Location(52.3738, 4.9123, "Oosterdok 2", "Amsterdam", "Nederland"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 16), AdminId),
        Place.Create("Artis Royal Zoo",
            "Historická zoo v centru města. Akvárium, planetárium, motýlí zahrada.",
            new Location(52.3660, 4.9163, "Plantage Kerklaan 38-40", "Amsterdam", "Nederland"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Vondelpark Playground",
            "Velké hřiště v nejznámějším amsterdamském parku. Pískoviště, skluzavky.",
            new Location(52.3580, 4.8686, "Vondelpark", "Amsterdam", "Nederland"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 10), AdminId),
        Place.Create("TunFun Speelpark",
            "Podzemní hřiště v bývalém autotunelu. Trampolíny, lezení, skluzavky.",
            new Location(52.3595, 4.9000, "Mr. Visserplein 7", "Amsterdam", "Nederland"),
            PlaceType.Playground, new KidsAttributes(true, true, true, 1, 12), AdminId),
        Place.Create("Pancakes Amsterdam — Centraal",
            "Palačinkárna s dětskými porcemi a omalovánkami. Přímo u nádraží.",
            new Location(52.3770, 4.9001, "Beursstraat 38", "Amsterdam", "Nederland"),
            PlaceType.Restaurant, new KidsAttributes(true, true, true, 0, 12), AdminId),
    };

    // ────────────── Varšava ──────────────
    private static List<Place> Warszawa() => new()
    {
        Place.Create("Centrum Nauki Kopernik",
            "Největší interaktivní vědecké centrum v Polsku. Planetárium.",
            new Location(52.2419, 21.0289, "Wybrzeże Kościuszkowskie 20", "Warszawa", "Polska"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 18), AdminId),
        Place.Create("Zoo Warszawa",
            "Historická zoo u Visly. Sloni, šelmy a dětský koutek.",
            new Location(52.2564, 21.0241, "Ratuszowa 1/3", "Warszawa", "Polska"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Park Łazienkowski — Playground",
            "Hřiště v krásném královském parku. Veverky, pávi, jezírko.",
            new Location(52.2147, 21.0355, "Agrykoli 1", "Warszawa", "Polska"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Wodny Park Moczydło",
            "Letní vodní park s bazény a tobogány. Oblíbený u rodin.",
            new Location(52.2239, 20.9522, "Górczewska 69/73", "Warszawa", "Polska"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Loopy's World Warszawa",
            "Velký indoor park pro děti. Trampolíny, prolézačky, tobogány.",
            new Location(52.2330, 20.9840, "Powsińska 31", "Warszawa", "Polska"),
            PlaceType.Playground, new KidsAttributes(true, true, true, 1, 12), AdminId),
    };

    // ────────────── Budapešť ──────────────
    private static List<Place> Budapest() => new()
    {
        Place.Create("Fővárosi Állat- és Növénykert",
            "Budapešťská zoo v Městském parku. Sloní dům, dětský koutek.",
            new Location(47.5176, 19.0822, "Állatkerti krt. 6-12", "Budapest", "Magyarország"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Palatinus Strandfürdő",
            "Letní koupaliště na Markétině ostrově. Vlnový bazén, tobogány.",
            new Location(47.5340, 19.0493, "Margitsziget", "Budapest", "Magyarország"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Városligeti Playground",
            "Velké moderní hřiště v Městském parku vedle zoo.",
            new Location(47.5155, 19.0832, "Városliget", "Budapest", "Magyarország"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 12), AdminId),
        Place.Create("Palace of Wonders (Csopa)",
            "Interaktivní vědecké centrum. Experimenty, optické iluze.",
            new Location(47.4742, 19.0640, "Nagytétényi út 37-43", "Budapest", "Magyarország"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 4, 16), AdminId),
        Place.Create("Margitsziget — Park",
            "Ostrov uprostřed Dunaje. Fontány, běžecká dráha, dětské hřiště.",
            new Location(47.5330, 19.0486, "Margitsziget", "Budapest", "Magyarország"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
    };

    // ────────────── Lisabon ──────────────
    private static List<Place> Lisboa() => new()
    {
        Place.Create("Oceanário de Lisboa",
            "Jedno z nejlepších akvárií na světě. Žraloci, vydry, rejnoci.",
            new Location(38.7636, -9.0938, "Esplanada Dom Carlos I", "Lisboa", "Portugal"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Jardim Zoológico de Lisboa",
            "Zoo s delfinárium a lanovkou. Přes 2000 zvířat.",
            new Location(38.7424, -9.1702, "Praça Marechal Humberto Delgado", "Lisboa", "Portugal"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Pavilhão do Conhecimento",
            "Interaktivní vědecké muzeum. Experimenty, stavění, voda.",
            new Location(38.7629, -9.0923, "Largo José Mariano Gago 1", "Lisboa", "Portugal"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 14), AdminId),
        Place.Create("Parque das Nações Playground",
            "Moderní hřiště u řeky v bývalém expo areálu.",
            new Location(38.7637, -9.0948, "Parque das Nações", "Lisboa", "Portugal"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Praia de Carcavelos",
            "Oblíbená rodinná pláž nedaleko Lisabonu. Bezpečný vstup do vody.",
            new Location(38.6775, -9.3353, "Carcavelos", "Lisboa", "Portugal"),
            PlaceType.Beach, new KidsAttributes(false, true, false, 0, 18), AdminId),
    };

    // ────────────── Brusel ──────────────
    private static List<Place> Bruxelles() => new()
    {
        Place.Create("Mini-Europe",
            "Miniaturní modely evropských památek. Interaktivní exponáty.",
            new Location(50.8950, 4.3395, "Bruparck", "Bruxelles", "Belgique"),
            PlaceType.Other, new KidsAttributes(true, true, true, 2, 14), AdminId),
        Place.Create("Museum of Natural Sciences",
            "Největší sbírka dinosaurů v Evropě. Interaktivní galerie.",
            new Location(50.8365, 4.3668, "Rue Vautier 29", "Bruxelles", "Belgique"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 16), AdminId),
        Place.Create("Parc de Bruxelles — Playground",
            "Hřiště v centrálním královském parku. Kolotoč a pískoviště.",
            new Location(50.8450, 4.3617, "Parc de Bruxelles", "Bruxelles", "Belgique"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 10), AdminId),
        Place.Create("Océade — Aquapark",
            "Tropický aquapark u Atomia. Tobogány, vlnový bazén.",
            new Location(50.8955, 4.3410, "Bruparck", "Bruxelles", "Belgique"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Bois de la Cambre",
            "Velký park na jihu Bruselu. Jezírko s lodičkami, hřiště, kavárna.",
            new Location(50.8102, 4.3740, "Bois de la Cambre", "Bruxelles", "Belgique"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
    };

    // ────────────── Stockholm ──────────────
    private static List<Place> Stockholm() => new()
    {
        Place.Create("Junibacken",
            "Svět Astrid Lindgrenové. Vláčkem přes příběhy Pipi a Emila.",
            new Location(59.3274, 18.0913, "Galärvarvsvägen 8", "Stockholm", "Sverige"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 0, 10), AdminId),
        Place.Create("Skansen",
            "Nejstarší skanzen na světě. Zoo se severskými zvířaty, řemesla.",
            new Location(59.3263, 18.1035, "Djurgårdsslätten 49-51", "Stockholm", "Sverige"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Gröna Lund",
            "Zábavní park na Djurgårdenu. Atrakce pro malé i velké.",
            new Location(59.3233, 18.0963, "Lilla Allmänna Gränd 9", "Stockholm", "Sverige"),
            PlaceType.Other, new KidsAttributes(true, true, true, 2, 18), AdminId),
        Place.Create("Naturhistoriska Riksmuseet",
            "Přírodovědné muzeum. Dinosauři, Cosmonova IMAX, polární expozice.",
            new Location(59.3693, 18.0542, "Frescativägen 40", "Stockholm", "Sverige"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 3, 16), AdminId),
        Place.Create("Vasaparken Playground",
            "Populární městské hřiště na Kungsholmenu. Vodní prvky v létě.",
            new Location(59.3380, 18.0375, "Vasaparken", "Stockholm", "Sverige"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 10), AdminId),
    };

    // ────────────── Kodaň ──────────────
    private static List<Place> Kobenhavn() => new()
    {
        Place.Create("Tivoli Gardens",
            "Legendární zábavní park z roku 1843. Atrakce, zahrady, představení.",
            new Location(55.6736, 12.5681, "Vesterbrogade 3", "København", "Danmark"),
            PlaceType.Other, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Experimentarium",
            "Interaktivní vědecké centrum. Bubliny, tunely, zvuk, lidské tělo.",
            new Location(55.7240, 12.5795, "Tuborg Havnevej 7", "København", "Danmark"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 16), AdminId),
        Place.Create("Copenhagen Zoo",
            "Moderní zoo s arktickou expozicí, sloní dům od Fostera.",
            new Location(55.6717, 12.5217, "Roskildevej 32", "København", "Danmark"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Fælledparken — Playground",
            "Obrovské hřiště v největším parku v Østerbro. Prolézačky, skejťák.",
            new Location(55.7016, 12.5699, "Fælledparken", "København", "Danmark"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 1, 14), AdminId),
        Place.Create("Den Blå Planet",
            "Největší akvárium v severní Evropě. Žraloci, korály, Amazonka.",
            new Location(55.6383, 12.6560, "Jacob Fortlingsvej 1", "København", "Danmark"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 0, 18), AdminId),
    };

    // ────────────── Helsinki ──────────────
    private static List<Place> Helsinki() => new()
    {
        Place.Create("Korkeasaari Zoo",
            "Zoo na ostrově. Sněžní leopardi, medvědi, amazonie.",
            new Location(60.1753, 24.9864, "Korkeasaari", "Helsinki", "Suomi"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Heureka — Finnish Science Centre",
            "Interaktivní vědecké muzeum. Planetárium, výstavy, experimenty.",
            new Location(60.2913, 25.0407, "Kuninkaalantie 7", "Helsinki", "Suomi"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 16), AdminId),
        Place.Create("Linnanmäki Amusement Park",
            "Zábavní park se vstupem zdarma. Atrakce, SEA LIFE akvárium.",
            new Location(60.1879, 24.9400, "Tivolikuja 1", "Helsinki", "Suomi"),
            PlaceType.Other, new KidsAttributes(true, true, true, 2, 18), AdminId),
        Place.Create("Leikkipuisto Arabianranta",
            "Moderní hřiště u moře v čtvrti Arabianranta.",
            new Location(60.2050, 24.9800, "Arabianranta", "Helsinki", "Suomi"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 10), AdminId),
        Place.Create("Serena Water Park",
            "Největší aquapark ve Finsku. Indoor i outdoor bazény.",
            new Location(60.3033, 24.6050, "Tornimäentie 10", "Helsinki", "Suomi"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
    };

    // ────────────── Oslo ──────────────
    private static List<Place> Oslo() => new()
    {
        Place.Create("TusenFryd",
            "Největší zábavní park v Norsku. Horské dráhy a dětská zóna.",
            new Location(59.7835, 10.7830, "Vinterbrovegen 25", "Oslo", "Norge"),
            PlaceType.Other, new KidsAttributes(true, true, true, 2, 18), AdminId),
        Place.Create("Norsk Teknisk Museum",
            "Technické muzeum. Interaktivní výstavy, energetika, zdraví.",
            new Location(59.9480, 10.7695, "Kjelsåsveien 143", "Oslo", "Norge"),
            PlaceType.Museum, new KidsAttributes(true, true, true, 3, 16), AdminId),
        Place.Create("Frognerparken — Vigelandsparken",
            "Slavný sochařský park. Obrovské trávníky a dětské hřiště.",
            new Location(59.9270, 10.7010, "Nobels gate 32", "Oslo", "Norge"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
        Place.Create("Reptilparken Oslo",
            "Malá zoo s plazy, obojživelníky a hmyzem. Dotykové programy.",
            new Location(59.9152, 10.7494, "St. Halvards gate 3", "Oslo", "Norge"),
            PlaceType.Zoo, new KidsAttributes(true, false, true, 3, 15), AdminId),
        Place.Create("Frognerbadet",
            "Venkovní koupaliště s olympijským bazénem a dětským brouzdalištěm.",
            new Location(59.9290, 10.6960, "Middelthuns gate 28", "Oslo", "Norge"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
    };

    // ────────────── Záhřeb ──────────────
    private static List<Place> Zagreb() => new()
    {
        Place.Create("Zoo Zagreb — Maksimir",
            "Zoo v parku Maksimir. Lvi, sloni, dětská farma.",
            new Location(45.8237, 16.0175, "Maksimirski perivoj", "Zagreb", "Hrvatska"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Tehnički muzej Nikola Tesla",
            "Technické muzeum. Planetárium, důl, Teslovy experimenty.",
            new Location(45.8031, 15.9625, "Savska cesta 18", "Zagreb", "Hrvatska"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 4, 16), AdminId),
        Place.Create("Bundek Park — Playground",
            "Velký park s jezerem a moderním hřištěm jižně od centra.",
            new Location(45.7864, 15.9863, "Bundek", "Zagreb", "Hrvatska"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 12), AdminId),
        Place.Create("Aquae Vivae — Krapinske Toplice",
            "Termální aquapark nedaleko Záhřebu. Tobogány a termální bazény.",
            new Location(46.0838, 15.8424, "Ulica Antuna Mihanovića 3", "Zagreb", "Hrvatska"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Park Maksimir",
            "Největší park v Záhřebu. Jezírka, stezky, dětská hřiště.",
            new Location(45.8261, 16.0184, "Maksimirski perivoj", "Zagreb", "Hrvatska"),
            PlaceType.Park, new KidsAttributes(false, false, true, 0, 18), AdminId),
    };

    // ────────────── Lublaň ──────────────
    private static List<Place> Ljubljana() => new()
    {
        Place.Create("Zoo Ljubljana",
            "Malá zoo na kopci Rožnik. Lvi, medvědi, dětská farma.",
            new Location(46.0523, 14.4715, "Večna pot 70", "Ljubljana", "Slovenija"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Tivoli Park — Playground",
            "Hlavní městský park s velkým hřištěm, rybníčky a cestami.",
            new Location(46.0574, 14.4876, "Park Tivoli", "Ljubljana", "Slovenija"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 12), AdminId),
        Place.Create("Hiša eksperimentov",
            "Interaktivní vědecké centrum. Fyzika, chemie, optika.",
            new Location(46.0421, 14.4887, "Trubarjeva cesta 39", "Ljubljana", "Slovenija"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 4, 14), AdminId),
        Place.Create("Atlantis Water Park",
            "Moderní aquapark s termální vodou. Tobogány a dětská zóna.",
            new Location(46.0657, 14.5373, "Šmartinska cesta 152", "Ljubljana", "Slovenija"),
            PlaceType.Pool, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Ljubljanski Grad — Hrad",
            "Hrad s výhledem na město. Loutkové divadlo a lanovka nahoru.",
            new Location(46.0489, 14.5085, "Grajska planota 1", "Ljubljana", "Slovenija"),
            PlaceType.Other, new KidsAttributes(false, false, false, 3, 18), AdminId),
    };

    // ────────────── Atény ──────────────
    private static List<Place> Athinai() => new()
    {
        Place.Create("Attica Zoological Park",
            "Největší zoo v Řecku. Delfinárium, ptačí park, farma.",
            new Location(37.9479, 23.8548, "Yalou", "Athina", "Elláda"),
            PlaceType.Zoo, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Hellenic Children's Museum",
            "Hands-on muzeum pro malé děti. Vaření, stavění, obchod.",
            new Location(37.9755, 23.7310, "Kydathineon 14", "Athina", "Elláda"),
            PlaceType.Museum, new KidsAttributes(true, false, true, 0, 10), AdminId),
        Place.Create("National Garden Playground",
            "Hřiště v Národní zahradě hned vedle parlamentu. Kachny, želvy.",
            new Location(37.9722, 23.7360, "Leoforos Vasilissis Amalias", "Athina", "Elláda"),
            PlaceType.Playground, new KidsAttributes(false, false, true, 0, 10), AdminId),
        Place.Create("Stavros Niarchos Foundation Cultural Center",
            "Moderní kulturní centrum. Vodní hřiště, zahrady, běžecká dráha.",
            new Location(37.9394, 23.6935, "Leof. Andrea Siggrou 364", "Athina", "Elláda"),
            PlaceType.Park, new KidsAttributes(true, true, true, 0, 18), AdminId),
        Place.Create("Vouliagmeni Beach",
            "Organizovaná rodinná pláž u Atén. Mělká voda, kavárna.",
            new Location(37.8117, 23.7834, "Vouliagmeni", "Athina", "Elláda"),
            PlaceType.Beach, new KidsAttributes(true, true, false, 0, 18), AdminId),
    };
}
