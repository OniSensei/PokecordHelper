Imports System.Net
Imports Discord
Imports Discord.WebSocket
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SQLite

Module Gotcha
    ' Our lists, all pokemon
    Dim pkmn() As String = {"Detective Pikachu", "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Alolan Rattata", "Raticate", "Alolan Raticate", "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu", "Raichu", "Alolan Raichu", "Sandshrew", "Alolan Sandshrew", "Sandslash", "Alolan Sandslash", "Nidoran", "Nidorina", "Nidoqueen", "Nidoran_m", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Alolan Vulpix", "Ninetales", "Alolan Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Alolan Diglett", "Dugtrio", "Alolan Dugtrio", "Meowth", "Alolan Meowth", "Persian", "Alolan Persian", "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag", "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel", "Geodude", "Alolan Geodude", "Graveler", "Alolan Graveler", "Golem", "Alolan Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetchd", "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Alolan Grimer", "Muk", "Alolan Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor", "Alolan Exeggutor", "Cubone", "Marowak", "Alolan Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing", "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr. Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew", "Chikorita", "Bayleef", "Meganium", "Cyndaquil", "Quilava", "Typhlosion", "Totodile", "Croconaw", "Feraligatr", "Sentret", "Furret", "Hoothoot", "Noctowl", "Ledyba", "Ledian", "Spinarak", "Ariados", "Crobat", "Chinchou", "Lanturn", "Pichu", "Cleffa", "Igglybuff", "Togepi", "Togetic", "Natu", "Xatu", "Mareep", "Flaaffy", "Ampharos", "Bellossom", "Marill", "Azumarill", "Sudowoodo", "Politoed", "Hoppip", "Skiploom", "Jumpluff", "Aipom", "Sunkern", "Sunflora", "Yanma", "Wooper", "Quagsire", "Espeon", "Umbreon", "Murkrow", "Slowking", "Misdreavus", "Unown", "Wobbuffet", "Girafarig", "Pineco", "Forretress", "Dunsparce", "Gligar", "Steelix", "Snubbull", "Granbull", "Qwilfish", "Scizor", "Shuckle", "Heracross", "Sneasel", "Teddiursa", "Ursaring", "Slugma", "Magcargo", "Swinub", "Piloswine", "Corsola", "Remoraid", "Octillery", "Delibird", "Mantine", "Skarmory", "Houndour", "Houndoom", "Kingdra", "Phanpy", "Donphan", "Porygon2", "Stantler", "Smeargle", "Tyrogue", "Hitmontop", "Smoochum", "Elekid", "Magby", "Miltank", "Blissey", "Raikou", "Entei", "Suicune", "Larvitar", "Pupitar", "Tyranitar", "Lugia", "Ho-Oh", "Celebi", "Treecko", "Grovyle", "Sceptile", "Torchic", "Combusken", "Blaziken", "Mudkip", "Marshtomp", "Swampert", "Poochyena", "Mightyena", "Zigzagoon", "Linoone", "Wurmple", "Silcoon", "Beautifly", "Cascoon", "Dustox", "Lotad", "Lombre", "Ludicolo", "Seedot", "Nuzleaf", "Shiftry", "Taillow", "Swellow", "Wingull", "Pelipper", "Ralts", "Kirlia", "Gardevoir", "Surskit", "Masquerain", "Shroomish", "Breloom", "Slakoth", "Vigoroth", "Slaking", "Nincada", "Ninjask", "Shedinja", "Whismur", "Loudred", "Exploud", "Makuhita", "Hariyama", "Azurill", "Nosepass", "Skitty", "Delcatty", "Sableye", "Mawile", "Aron", "Lairon", "Aggron", "Meditite", "Medicham", "Electrike", "Manectric", "Plusle", "Minun", "Volbeat", "Illumise", "Roselia", "Gulpin", "Swalot", "Carvanha", "Sharpedo", "Wailmer", "Wailord", "Numel", "Camerupt", "Torkoal", "Spoink", "Grumpig", "Spinda", "Trapinch", "Vibrava", "Flygon", "Cacnea", "Cacturne", "Swablu", "Altaria", "Zangoose", "Seviper", "Lunatone", "Solrock", "Barboach", "Whiscash", "Corphish", "Crawdaunt", "Baltoy", "Claydol", "Lileep", "Cradily", "Anorith", "Armaldo", "Feebas", "Milotic", "Castform", "Kecleon", "Shuppet", "Banette", "Duskull", "Dusclops", "Tropius", "Chimecho", "Absol", "Wynaut", "Snorunt", "Glalie", "Spheal", "Sealeo", "Walrein", "Clamperl", "Huntail", "Gorebyss", "Relicanth", "Luvdisc", "Bagon", "Shelgon", "Salamence", "Beldum", "Metang", "Metagross", "Regirock", "Regice", "Registeel", "Latias", "Latios", "Kyogre", "Groudon", "Rayquaza", "Jirachi", "Deoxys", "Turtwig", "Grotle", "Torterra", "Chimchar", "Monferno", "Infernape", "Piplup", "Prinplup", "Empoleon", "Starly", "Staravia", "Staraptor", "Bidoof", "Bibarel", "Kricketot", "Kricketune", "Shinx", "Luxio", "Luxray", "Budew", "Roserade", "Cranidos", "Rampardos", "Shieldon", "Bastiodon", "Burmy", "Wormadam", "Mothim", "Combee", "Vespiquen", "Pachirisu", "Buizel", "Floatzel", "Cherubi", "Cherrim", "Shellos", "Gastrodon", "Ambipom", "Drifloon", "Drifblim", "Buneary", "Lopunny", "Mismagius", "Honchkrow", "Glameow", "Purugly", "Chingling", "Stunky", "Skuntank", "Bronzor", "Bronzong", "Bonsly", "Mime Jr.", "Happiny", "Chatot", "Spiritomb", "Gible", "Gabite", "Garchomp", "Munchlax", "Riolu", "Lucario", "Hippopotas", "Hippowdon", "Skorupi", "Drapion", "Croagunk", "Toxicroak", "Carnivine", "Finneon", "Lumineon", "Mantyke", "Snover", "Abomasnow", "Weavile", "Magnezone", "Lickilicky", "Rhyperior", "Tangrowth", "Electivire", "Magmortar", "Togekiss", "Yanmega", "Leafeon", "Glaceon", "Gliscor", "Mamoswine", "Porygon-Z", "Gallade", "Probopass", "Dusknoir", "Froslass", "Rotom", "Uxie", "Mesprit", "Azelf", "Dialga", "Palkia", "Heatran", "Regigigas", "Giratina", "Cresselia", "Phione", "Manaphy", "Darkrai", "Shaymin", "Arceus", "Victini", "Snivy", "Servine", "Serperior", "Tepig", "Pignite", "Emboar", "Oshawott", "Dewott", "Samurott", "Patrat", "Watchog", "Lillipup", "Herdier", "Stoutland", "Purrloin", "Liepard", "Pansage", "Simisage", "Pansear", "Simisear", "Panpour", "Simipour", "Munna", "Musharna", "Pidove", "Tranquill", "Unfezant", "Blitzle", "Zebstrika", "Roggenrola", "Boldore", "Gigalith", "Woobat", "Swoobat", "Drilbur", "Excadrill", "Audino", "Timburr", "Gurdurr", "Conkeldurr", "Tympole", "Palpitoad", "Seismitoad", "Throh", "Sawk", "Sewaddle", "Swadloon", "Leavanny", "Venipede", "Whirlipede", "Scolipede", "Cottonee", "Whimsicott", "Petilil", "Lilligant", "Basculin", "Sandile", "Krokorok", "Krookodile", "Darumaka", "Darmanitan", "Maractus", "Dwebble", "Crustle", "Scraggy", "Scrafty", "Sigilyph", "Yamask", "Cofagrigus", "Tirtouga", "Carracosta", "Archen", "Archeops", "Trubbish", "Garbodor", "Zorua", "Zoroark", "Minccino", "Cinccino", "Gothita", "Gothorita", "Gothitelle", "Solosis", "Duosion", "Reuniclus", "Ducklett", "Swanna", "Vanillite", "Vanillish", "Vanilluxe", "Deerling", "Sawsbuck", "Emolga", "Karrablast", "Escavalier", "Foongus", "Amoonguss", "Frillish", "Jellicent", "Alomomola", "Joltik", "Galvantula", "Ferroseed", "Ferrothorn", "Klink", "Klang", "Klinklang", "Tynamo", "Eelektrik", "Eelektross", "Elgyem", "Beheeyem", "Litwick", "Lampent", "Chandelure", "Axew", "Fraxure", "Haxorus", "Cubchoo", "Beartic", "Cryogonal", "Shelmet", "Accelgor", "Stunfisk", "Mienfoo", "Mienshao", "Druddigon", "Golett", "Golurk", "Pawniard", "Bisharp", "Bouffalant", "Rufflet", "Braviary", "Vullaby", "Mandibuzz", "Heatmor", "Durant", "Deino", "Zweilous", "Hydreigon", "Larvesta", "Volcarona", "Cobalion", "Terrakion", "Virizion", "Tornadus", "Thundurus", "Reshiram", "Zekrom", "Landorus", "Kyurem", "Keldeo", "Meloetta", "Genesect", "Chespin", "Quilladin", "Chesnaught", "Fennekin", "Braixen", "Delphox", "Froakie", "Frogadier", "Greninja", "Bunnelby", "Diggersby", "Fletchling", "Fletchinder", "Talonflame", "Scatterbug", "Spewpa", "Vivillon", "Litleo", "Pyroar", "Flabebe", "Floette", "Florges", "Skiddo", "Gogoat", "Pancham", "Pangoro", "Furfrou", "Espurr", "Meowstic", "Honedge", "Doublade", "Aegislash", "Spritzee", "Aromatisse", "Swirlix", "Slurpuff", "Inkay", "Malamar", "Binacle", "Barbaracle", "Skrelp", "Dragalge", "Clauncher", "Clawitzer", "Helioptile", "Heliolisk", "Tyrunt", "Tyrantrum", "Amaura", "Aurorus", "Sylveon", "Hawlucha", "Dedenne", "Carbink", "Goomy", "Sliggoo", "Goodra", "Klefki", "Phantump", "Trevenant", "Pumpkaboo", "Gourgeist", "Bergmite", "Avalugg", "Noibat", "Noivern", "Xerneas", "Yveltal", "Zygarde", "Diancie", "Hoopa", "Volcanion", "Rowlet", "Dartrix", "Decidueye", "Litten", "Torracat", "Incineroar", "Popplio", "Brionne", "Primarina", "Pikipek", "Trumbeak", "Toucannon", "Yungoos", "Gumshoos", "Grubbin", "Charjabug", "Vikavolt", "Crabrawler", "Crabominable", "Oricorio", "Cutiefly", "Ribombee", "Rockruff", "Lycanroc", "Wishiwashi", "Mareanie", "Toxapex", "Mudbray", "Mudsdale", "Dewpider", "Araquanid", "Fomantis", "Lurantis", "Morelull", "Shiinotic", "Salandit", "Salazzle", "Stufful", "Bewear", "Bounsweet", "Steenee", "Tsareena", "Comfey", "Oranguru", "Passimian", "Wimpod", "Golisopod", "Sandygast", "Palossand", "Pyukumuku", "Type: Null", "Silvally", "Minior", "Komala", "Turtonator", "Togedemaru", "Mimikyu", "Bruxish", "Drampa", "Dhelmise", "Jangmo-o", "Hakamo-o", "Kommo-o", "Tapu Koko", "Tapu Lele", "Tapu Bulu", "Tapu Fini", "Cosmog", "Cosmoem", "Solgaleo", "Lunala", "Nihilego", "Buzzwole", "Pheromosa", "Xurkitree", "Celesteela", "Kartana", "Guzzlord", "Necrozma", "Magearna", "Marshadow", "Poipole", "Naganadel", "Stakataka", "Blacephalon", "Zeraora", "Meltan", "Melmetal"}
    Dim pokecordstatus As Boolean = True
    Dim prefix As String = "ph!"
    Dim token As String = ""
    Dim path As String = Application.StartupPath
    Dim pokepath As String = path & "\poke\"
    Dim guildpath As String = path & "\guilds\"
    Dim sqlite_conn As SQLiteConnection

    ' Main sub
    Sub Main(args As String())
        handler = New ConsoleEventDelegate(AddressOf ConsoleEventCallback)
        SetConsoleCtrlHandler(handler, True)

        ' Start main as an async sub
        MainAsync.GetAwaiter.GetResult()
    End Sub

    ' Set discord client variable
    Public _client As DiscordSocketClient = New DiscordSocketClient

    ' New sub
    Sub New()
        ' Set console encoding for names with symbols like nidoran♂️ and nidoran♀️
        Console.OutputEncoding = Encoding.UTF8
        ' Set our log, ready, timer, and message received functions
        AddHandler _client.Log, AddressOf LogAsync
        AddHandler _client.Ready, AddressOf ReadAsync
        AddHandler _client.MessageReceived, AddressOf MessageReceivedAsync
    End Sub

    ' Async main function as referenced above
    ' Set the STA Thread
    <STAThread()>
    Public Async Function MainAsync() As Task
        Colorize("Loading Pokecord Helper i.e `Bill's PC`")

        ' Set thread
        Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.STA)

        Try
            'NjYwNjEzOTU3NTg4ODc3MzM0.Xgfaxw.X5wz2RRWzW326ur3bWdPnP8RbpU
            token = My.Computer.FileSystem.ReadAllText(path & "\token.txt")
            Await _client.LoginAsync(TokenType.Bot, token)
        Catch ex As Exception
            Colorize("[INFO]      Invalid Token")
        End Try

        ' Wait for the client to start
        Await _client.StartAsync
        Await Task.Delay(-1)
    End Function

    ' Log discord.net messages
    Private Async Function LogAsync(ByVal log As LogMessage) As Task(Of Task)
        ' Once loginasync and startasync finish we get the log message of "Ready" once we get that, we load everything else
        If log.ToString.Contains("Ready") Then
            Colorize("[INFO]      Bot started.")
        ElseIf log.ToString.Contains("gateway") Or log.ToString.Contains("unhandled") Then
            ' dont log, people dont need to see it
        Else
            Colorize("[LOAD]      " & log.ToString) ' update console
        End If
        Return Task.CompletedTask
    End Function

    ' Async reader
    Private Function ReadAsync() As Task
        Return Task.CompletedTask
    End Function

    ' Async message revieved function
    Private Async Function MessageReceivedAsync(ByVal message As SocketMessage) As Task
        Dim c As SocketGuildChannel = message.Channel
        Dim g As IGuild = c.Guild

        If My.Computer.FileSystem.FileExists(guildpath & g.Id & ".db") = False Then
            Try
                sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                Dim sqlite_cmd As SQLiteCommand

                ' open the connection:
                sqlite_conn.Open()

                sqlite_cmd = sqlite_conn.CreateCommand()

                ' Let the SQLiteCommand object know our SQL-Query:
                sqlite_cmd.CommandText = "CREATE TABLE permissions (id Integer PRIMARY KEY, userid Text Not NULL DEFAULT '" & _client.GetGuild(g.Id).OwnerId & "');"
                sqlite_cmd.ExecuteNonQuery()

                sqlite_cmd.CommandText = "CREATE TABLE guild (id Integer PRIMARY KEY, whitelist Text Not NULL DEFAULT '" & c.Id & "', prefix Text Not NULL DEFAULT 'ph!', autopost Text Not NULL DEFAULT 'False', usespoilers Text Not NULL DEFAULT 'True');"
                sqlite_cmd.ExecuteNonQuery()

                sqlite_cmd.CommandText = "INSERT INTO permissions (userid) VALUES ('" & _client.GetGuild(g.Id).OwnerId & "');"
                sqlite_cmd.ExecuteNonQuery()

                sqlite_cmd.CommandText = "INSERT INTO guild (autopost) VALUES ('True');"
                sqlite_cmd.ExecuteNonQuery()

                Colorize("[SQL]       Created new SQLite DB for guild " & g.Id)
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
            End Try
        End If

        If message.Author.Id = "365975655608745985" = True Then
            If CheckChannel(c.Id, g.Id) = True Then
                ' Encounter
                If message.Author.IsBot And message.Embeds(0).Description.Contains("Guess the pokémon") Then
                    ' Download the image into memory for conversion to base64
                    Dim url As String = message.Embeds(0).Image.ToString ' URL of image from pokemon spawn on discord
                    Dim tClient As WebClient = New WebClient
                    Dim timage As Bitmap = Drawing.Image.FromStream(New MemoryStream(tClient.DownloadData(url)))

                    Dim pokemon As String = ConvertImageToBase64(timage) ' Uses convertimagetobase64 function below with the image as reference

                    ' Search for pokemons base64 string
                    Try
                        For i = 0 To (pkmn.Count - 1) ' loop
                            Dim di As New DirectoryInfo(pokepath) ' set the directory of the pokemon base64 files
                            Dim pokemonName As String
                            For Each fi As FileInfo In di.GetFiles() ' get the file name of each file i n the directory
                                If File.ReadAllText(fi.FullName).Contains(pokemon) Then ' if the file contains the base64 string then
                                    Try
                                        pokemonName = fi.Name
                                        pokemonName = pokemonName.Remove(pokemonName.Length - 4) ' clean up the ".txt" from end of file name
                                        If pokemonName = "TypeNull" Then pokemonName = "Type: Null" ' cleanup
                                        If pokemonName = "Nidoran_m" Then pokemonName = "Nidoran" ' cleanup
                                        Dim e As EmbedBuilder = New EmbedBuilder

                                        If CheckAutoPost(g.Id) = True Then
                                            If CheckSpoilers(g.Id) = True Then
                                                e.WithTitle("Bill's PC:")
                                                e.WithColor(247, 202, 0)
                                                e.WithDescription("||" & pokemonName.ToLower & "||")
                                                Colorize("[INFO]      " & Date.Now & " | " & g.Name & " : " & g.Id & " Pokemon identified as " & pokemonName.ToLower & " in " & g.Name)
                                            Else
                                                e.WithTitle("Bill's PC:")
                                                e.WithColor(247, 202, 0)
                                                e.WithDescription(pokemonName.ToLower)
                                                Colorize("[INFO]      Pokemon identified as " & pokemonName.ToLower & " in " & g.Name)
                                            End If
                                            Await message.Channel.SendMessageAsync("", False, e.Build)
                                        End If

                                        Exit For
                                    Catch ex As Exception
                                        Console.WriteLine(ex.ToString) ' error
                                    End Try
                                End If
                            Next
                            Exit For
                        Next
                    Catch ex As Exception
                        Console.WriteLine(ex.ToString) 'error
                    End Try
                End If
            End If

            ' Commands
        Else
            prefix = GetPrefix(g.Id)
            If message.Content.ToLower.StartsWith(prefix & "start") Then
                Try
                    ' Add channel to whitelist
                    Dim e As EmbedBuilder = New EmbedBuilder

                    If CheckPermissions(message.Author.Id, g) = True Then
                        Dim newwhite As String = GetWhitelist(g.Id) & "," & message.Channel.Id

                        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                        Dim sqlite_cmd As SQLiteCommand

                        ' open the connection:
                        sqlite_conn.Open()

                        sqlite_cmd = sqlite_conn.CreateCommand()

                        sqlite_cmd.CommandText = "UPDATE guild SET whitelist = '" & newwhite & "';"
                        sqlite_cmd.ExecuteNonQuery()
                        Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " Added Channel: " & c.Name & " : " & c.Id)

                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " added <#" & message.Channel.Id & "> to the server whitelist.")
                        e.WithColor(53, 156, 91)

                    Else
                        e.WithTitle("Bill's PC:")
                        e.WithColor(150, 14, 14)
                        e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "add` command.")
                    End If

                    Await message.Channel.SendMessageAsync("", False, e.Build)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try
            ElseIf message.Content.ToLower.StartsWith(prefix & "ping") Then
                Dim ping As String = _client.Latency
                Dim e As EmbedBuilder = New EmbedBuilder
                e.WithTitle("Bill's PC:")
                e.WithColor(247, 202, 0)
                e.WithDescription("Ping: " & ping & "ms")
                Colorize("[INFO]      " & Date.Now & " | Ping: " & ping & "ms")

                Await message.Channel.SendMessageAsync("", False, e.Build)
            ElseIf message.Content.ToLower.StartsWith(prefix & "help") Then
                ' Help command
                Dim e As EmbedBuilder = New EmbedBuilder
                e.WithTitle("Bill's PC:")
                e.WithColor(247, 202, 0)
                e.WithDescription("Thank you for using Bill's PC, the pokecord helper. Use `" & prefix & "help` to see these options anytime.")
                e.AddField("Current Prefix: `" & prefix & "`", "This is the current bot prefix.", False)
                e.AddField("**Get the help page.**", "`" & prefix & "help`", True)
                e.AddField("**Give permissions to user.**", "`" & prefix & "giveperm <@username>`", True)
                e.AddField("**Remove permissions from user.**", "`" & prefix & "takeperms <@username>`", True)
                e.AddField("**Change the prefix.**", "`" & prefix & "prefix <newprefix>`", True)
                e.AddField("**Start the bot in the server & adds the first channel.**", "`" & prefix & "start`", True)
                e.AddField("**Adds the channel to the bot.**", "`" & prefix & "add <#channel>`", True)
                e.AddField("**Removes the channel from the bot.**", "`" & prefix & "rem <#channel>`", True)
                e.AddField("**Check the bots ping.**", "`" & prefix & "ping`", True)
                e.AddField("**Toggle autopost on/off.**", "`" & prefix & "autopost`", True)
                e.AddField("**Toggle spoilers on/off.**", "`" & prefix & "spoilers`", True)
                'e.AddField("**Manual pokemon check.**", "`" & prefix & "check <messageid>`", True)

                Await message.Channel.SendMessageAsync("", False, e.Build)
            ElseIf message.Content.ToLower.StartsWith(prefix & "prefix") Then
                Dim e As EmbedBuilder = New EmbedBuilder

                Dim content As String = message.Content
                Dim params() As String = content.Split(" ")
                If params.Count = 2 Then
                    If CheckPermissions(message.Author.Id, g) = True Then
                        UpdatePrefix(params(1), g.Id)
                        e.WithTitle("Bill's PC:")
                        e.WithColor(53, 156, 91)
                        e.WithDescription(message.Author.Mention & " prefix has been updated to " & params(1))
                    Else
                        e.WithTitle("Bill's PC:")
                        e.WithColor(150, 14, 14)
                        e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "prefix` command.")
                    End If
                Else
                    e.WithTitle("Bill's PC:")
                    e.WithColor(150, 14, 14)
                    e.WithDescription(message.Author.Mention & " incorrect command parameters.")
                End If

                Await message.Channel.SendMessageAsync("", False, e.Build)
            ElseIf message.Content.ToLower.StartsWith(prefix & "takeperm") Then
                ' Take perm
                Dim e As EmbedBuilder = New EmbedBuilder

                If CheckPermissions(message.Author.Id, g) = True Then
                    Dim userid As String = message.MentionedUsers.FirstOrDefault.Id

                    If userid = g.OwnerId Then
                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " You are the server owner, you can't do this action.")
                        e.WithColor(150, 14, 14)
                    ElseIf userid = message.Author.Id Then
                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " You can not modify your own permissions.")
                        e.WithColor(150, 14, 14)
                    Else
                        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                        Dim sqlite_cmd As SQLiteCommand

                        ' open the connection:
                        sqlite_conn.Open()

                        sqlite_cmd = sqlite_conn.CreateCommand()

                        sqlite_cmd.CommandText = "DELETE FROM permissions WHERE userid = '" & userid & "';"
                        sqlite_cmd.ExecuteNonQuery()
                        Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Permissions taken from UserID:" & userid)

                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " Permissions taken from <@" & userid & ">")
                        e.WithColor(53, 156, 91)
                    End If
                Else
                    e.WithTitle("Bill's PC:")
                    e.WithColor(150, 14, 14)
                    e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "takeperm` command.")
                End If

                Await message.Channel.SendMessageAsync("", False, e.Build)
            ElseIf message.Content.ToLower.StartsWith(prefix & "giveperm") = True Then
                ' Give permissions
                Dim e As EmbedBuilder = New EmbedBuilder

                If CheckPermissions(message.Author.Id, g) = True Then
                    Dim userid As String = message.MentionedUsers.FirstOrDefault.Id

                    If userid = g.OwnerId Then
                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " You are the server owner, you can't do this action.")
                        e.WithColor(150, 14, 14)
                    ElseIf userid = message.Author.Id Then
                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " You can not modify your own permissions.")
                        e.WithColor(150, 14, 14)
                    Else
                        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                        Dim sqlite_cmd As SQLiteCommand

                        ' open the connection:
                        sqlite_conn.Open()

                        sqlite_cmd = sqlite_conn.CreateCommand()

                        sqlite_cmd.CommandText = "INSERT INTO permissions (userid) VALUES ('" & userid & "');"
                        sqlite_cmd.ExecuteNonQuery()
                        Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Permissions given to UserID:" & userid)

                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " Permissions given tp <@" & userid & ">")
                        e.WithColor(53, 156, 91)
                    End If
                Else
                    e.WithTitle("Bill's PC:")
                    e.WithColor(150, 14, 14)
                    e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "giveperm` command.")
                End If

                Await message.Channel.SendMessageAsync("", False, e.Build)

            ElseIf message.Content.ToLower.StartsWith(prefix & "add") = True Then
                Try
                    ' Add channel to whitelist
                    Dim e As EmbedBuilder = New EmbedBuilder

                    If CheckPermissions(message.Author.Id, g) = True Then
                        Dim newwhite As String = GetWhitelist(g.Id) & "," & message.MentionedChannels.FirstOrDefault.Id

                        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                        Dim sqlite_cmd As SQLiteCommand

                        ' open the connection:
                        sqlite_conn.Open()

                        sqlite_cmd = sqlite_conn.CreateCommand()

                        sqlite_cmd.CommandText = "UPDATE guild SET whitelist = '" & newwhite & "';"
                        sqlite_cmd.ExecuteNonQuery()
                        Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Added Channel: " & message.MentionedChannels.FirstOrDefault.Name & " : " & message.MentionedChannels.FirstOrDefault.Id)

                        e.WithTitle("Bill's PC:")
                        e.WithDescription(message.Author.Mention & " added <#" & message.MentionedChannels.FirstOrDefault.Id & "> to the server whitelist.")
                        e.WithColor(53, 156, 91)

                    Else
                        e.WithTitle("Bill's PC:")
                        e.WithColor(150, 14, 14)
                        e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "add` command.")
                    End If

                    Await message.Channel.SendMessageAsync("", False, e.Build)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try
            ElseIf message.Content.ToLower.StartsWith(prefix & "rem") = True Then
                Try
                    ' Remove channel from whitelist
                    Dim e As EmbedBuilder = New EmbedBuilder

                    If CheckPermissions(message.Author.Id, g) = True Then
                        Dim white As String = GetWhitelist(g.Id)
                        Dim listWhite As String() = white.Split(",")
                        Dim whitelist As List(Of String) = New List(Of String)(listWhite)

                        For i As Integer = 0 To whitelist.Count - 1
                            If whitelist(i).ToString = message.MentionedChannels.FirstOrDefault.Id Then
                                whitelist.RemoveAt(i)
                                sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                                Dim sqlite_cmd As SQLiteCommand

                                ' open the connection:
                                sqlite_conn.Open()

                                sqlite_cmd = sqlite_conn.CreateCommand()

                                sqlite_cmd.CommandText = "UPDATE guild SET whitelist = '" & String.Join(",", whitelist.ToArray()) & "';"
                                sqlite_cmd.ExecuteNonQuery()
                                Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Removed Channel: " & message.MentionedChannels.FirstOrDefault.Name & " : " & message.MentionedChannels.FirstOrDefault.Id)

                                e.WithTitle("Bill's PC:")
                                e.WithDescription(message.Author.Mention & " removed <#" & message.MentionedChannels.FirstOrDefault.Id & "> from the server whitelist.")
                                e.WithColor(53, 156, 91)
                            End If
                        Next
                    Else
                        e.WithTitle("Bill's PC:")
                        e.WithColor(150, 14, 14)
                        e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "add` command.")
                    End If

                    Await message.Channel.SendMessageAsync("", False, e.Build)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try
            ElseIf message.Content.ToLower.StartsWith(prefix & "autopost") = True Then
                Try
                    ' Toggle autopost
                    Dim e As EmbedBuilder = New EmbedBuilder

                    If CheckPermissions(message.Author.Id, g) = True Then
                        If CheckAutoPost(g.Id) = True Then
                            sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                            Dim sqlite_cmd As SQLiteCommand

                            ' open the connection:
                            sqlite_conn.Open()

                            sqlite_cmd = sqlite_conn.CreateCommand()

                            sqlite_cmd.CommandText = "UPDATE guild SET autopost = 'False';"
                            sqlite_cmd.ExecuteNonQuery()

                            Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Autopost set to False.")

                            e.WithTitle("Bill's PC:")
                            e.WithDescription(message.Author.Mention & " autopost has been disabled.")
                            e.WithColor(150, 14, 14)
                        Else
                            sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                            Dim sqlite_cmd As SQLiteCommand

                            ' open the connection:
                            sqlite_conn.Open()

                            sqlite_cmd = sqlite_conn.CreateCommand()

                            sqlite_cmd.CommandText = "UPDATE guild SET autopost = 'True';"
                            sqlite_cmd.ExecuteNonQuery()

                            Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Autopost set to True.")

                            e.WithTitle("Bill's PC:")
                            e.WithDescription(message.Author.Mention & " autopost has been enabled.")
                            e.WithColor(53, 156, 91)
                        End If
                    Else
                        e.WithTitle("Bill's PC:")
                        e.WithColor(150, 14, 14)
                        e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "add` command.")
                    End If

                    Await message.Channel.SendMessageAsync("", False, e.Build)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try
            ElseIf message.Content.ToLower.StartsWith(prefix & "spoilers") = True Then
                Try
                    ' Toggle spoilers
                    Dim e As EmbedBuilder = New EmbedBuilder

                    If CheckPermissions(message.Author.Id, g) = True Then
                        If CheckSpoilers(g.Id) = True Then
                            sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                            Dim sqlite_cmd As SQLiteCommand

                            ' open the connection:
                            sqlite_conn.Open()

                            sqlite_cmd = sqlite_conn.CreateCommand()

                            sqlite_cmd.CommandText = "UPDATE guild SET usespoilers = 'False';"
                            sqlite_cmd.ExecuteNonQuery()

                            Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Spoilers set to False.")

                            e.WithTitle("Bill's PC:")
                            e.WithDescription(message.Author.Mention & " spoilers have been disabled.")
                            e.WithColor(150, 14, 14)
                        Else
                            sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & g.Id & ".db;Version=3;")
                            Dim sqlite_cmd As SQLiteCommand

                            ' open the connection:
                            sqlite_conn.Open()

                            sqlite_cmd = sqlite_conn.CreateCommand()

                            sqlite_cmd.CommandText = "UPDATE guild SET usespoilers = 'True';"
                            sqlite_cmd.ExecuteNonQuery()

                            Colorize("[SQL]       " & Date.Now & " | " & g.Name & " : " & g.Id & " | Spolers set to True.")

                            e.WithTitle("Bill's PC:")
                            e.WithDescription(message.Author.Mention & " spolers have been enabled.")
                            e.WithColor(53, 156, 91)
                        End If
                    Else
                        e.WithTitle("Bill's PC:")
                        e.WithColor(150, 14, 14)
                        e.WithDescription(message.Author.Mention & " you do not have permission use the `" & prefix & "add` command.")
                    End If

                    Await message.Channel.SendMessageAsync("", False, e.Build)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try
            Else
                If message.Author.IsBot = False Then
                    Colorize("[CHAT]      " & Date.Now & " | [" & g.Name & "]" & message.Author.Username & ": " & message.Content)
                End If
            End If
        End If
    End Function

    ' Convert image to base64
    Public Function ConvertImageToBase64(ImageInput As Drawing.Image) As String
        Using ms As New MemoryStream()
            ImageInput.Save(ms, ImageInput.RawFormat)
            Dim imageBytes As Byte() = ms.ToArray
            Dim base64String As String = Convert.ToBase64String(imageBytes)

            Return base64String
        End Using
    End Function

    ' Colorize function for console font color
    Public Sub Colorize(ByVal msg As String)
        ' Checks the message for particular string and changes the color, then updates the log
        Select Case True
            Case msg.Contains("ERROR") ' error message
                Console.ForegroundColor = ConsoleColor.DarkRed ' errors are red
                Console.WriteLine(msg) ' update console
                Console.ResetColor() ' reset the color
            Case msg.Contains("INFO") ' repeat
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("ENCOUNTER")
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("CHAT")
                Console.ForegroundColor = ConsoleColor.DarkCyan
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("EVOLVE")
                Console.ForegroundColor = ConsoleColor.DarkCyan
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("CATCH")
                Console.ForegroundColor = ConsoleColor.DarkGreen
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("LOAD")
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("STAT")
                Console.ForegroundColor = ConsoleColor.DarkGray
                Console.WriteLine(msg)
                Console.ResetColor()
            Case Else
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Console.WriteLine(msg)
                Console.ResetColor()
        End Select
    End Sub

    Private Function ConsoleEventCallback(ByVal eventType As Integer) As Boolean
        Select Case eventType
            Case 0
                ' Update user count
                Try
                    Colorize("[INFO]      " & Date.Now & " | Bot Closing | Prepairing final tasks and saving")

                    _client.Dispose()
                Catch ex As Exception
                End Try
            Case 1
                ' Update user count
                Try
                    Colorize("[INFO]      " & Date.Now & " | Bot Closing | Prepairing final tasks and saving")

                    _client.Dispose()
                Catch ex As Exception
                End Try
            Case 2
                ' Update user count
                Try
                    Colorize("[INFO]      " & Date.Now & " | Bot Closing | Prepairing final tasks and saving")

                    _client.Dispose()
                Catch ex As Exception
                End Try
            Case 5
                ' Update user count
                Try
                    Colorize("[INFO]      " & Date.Now & " | Bot Closing | Prepairing final tasks and saving")

                    _client.Dispose()
                Catch ex As Exception
                End Try
            Case 6
                ' Update user count
                Try
                    Colorize("[INFO]      " & Date.Now & " | Bot Closing | Prepairing final tasks and saving")

                    _client.Dispose()
                Catch ex As Exception
                End Try
        End Select
        Return False
    End Function

    Dim handler As ConsoleEventDelegate
    Private Delegate Function ConsoleEventDelegate(ByVal eventType As Integer) As Boolean
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function SetConsoleCtrlHandler(ByVal callback As ConsoleEventDelegate, ByVal add As Boolean) As Boolean
    End Function

    Public Function CheckPermissions(ByVal authorid As String, ByVal guild As IGuild) As Boolean
        Dim hasperm As Boolean = False

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guild.Id & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT count(*) FROM permissions WHERE userid = '" & authorid & "';"
        Dim hasrows = Convert.ToInt32(sqlite_cmd.ExecuteScalar())

        If hasrows >= 1 Then
            hasperm = True
        End If

        Return hasperm
    End Function

    Public Function CheckChannel(ByVal channelid As String, ByVal guildid As String) As Boolean
        Dim haspoke As Boolean = False
        Dim whitelist As String

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT whitelist FROM guild;"
        whitelist = sqlite_cmd.ExecuteScalar()

        If whitelist.Contains(channelid) Then
            haspoke = True
        End If

        Return haspoke
    End Function

    Public Function CheckAutoPost(ByVal guildid As String) As Boolean
        Dim doauto As Boolean = False
        Dim autopost As String

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT autopost FROM guild;"
        autopost = sqlite_cmd.ExecuteScalar()

        If autopost = "True" Then
            doauto = True
        End If

        Return doauto
    End Function

    Public Function CheckSpoilers(ByVal guildid As String) As Boolean
        Dim dospoil As Boolean = False
        Dim usespoilers As String

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT usespoilers FROM guild;"
        usespoilers = sqlite_cmd.ExecuteScalar()

        If usespoilers = "True" Then
            dospoil = True
        End If

        Return dospoil
    End Function

    Public Function CheckTable(ByVal guildid As String) As Boolean
        Dim hastable As Boolean = False

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT usespoilers FROM guild;"
        sqlite_cmd.ExecuteScalar()

        Dim hasrows = Convert.ToInt32(sqlite_cmd.ExecuteScalar())

        If hasrows >= 1 Then
            hastable = True
        End If

        Return hastable
    End Function

    Public Sub UpdatePrefix(ByVal newprefix As String, ByVal guildid As String)
        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "UPDATE guild SET prefix = '" & newprefix & "';"
        sqlite_cmd.ExecuteNonQuery()
        Colorize("[SQL]       " & Date.Now & " " & guildid & " | Updated Prefix to:" & newprefix)
    End Sub

    Public Function GetPrefix(ByVal guildid As String) As String
        Dim theprefix As String = "ph!"

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT prefix FROM guild;"
        theprefix = sqlite_cmd.ExecuteScalar()

        Return theprefix
    End Function

    Public Function GetWhitelist(ByVal guildid As String) As String
        Dim whitelist As String = ""

        sqlite_conn = New SQLiteConnection("Data Source=" & guildpath & guildid & ".db;Version=3;")
        Dim sqlite_cmd As SQLiteCommand

        ' open the connection:
        sqlite_conn.Open()

        sqlite_cmd = sqlite_conn.CreateCommand()

        sqlite_cmd.CommandText = "SELECT whitelist FROM guild;"
        whitelist = sqlite_cmd.ExecuteScalar()

        Return whitelist
    End Function
End Module
