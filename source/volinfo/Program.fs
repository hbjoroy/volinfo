module Program
open Argu
open Volume

type Arguments = 
    | [<CliPrefix(CliPrefix.None)>]Help
    | [<CliPrefix(CliPrefix.None)>]ListVolumes
    | [<CliPrefix(CliPrefix.None)>]VolumeInfo of string
    | [<CliPrefix(CliPrefix.None)>]FolderInfo of string
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | ListVolumes -> "Usage: volinfo listvolumes\n\nList all volumes on the system."
            | VolumeInfo _ -> "Usage: volinfo volumeinfo <volume>\n\nGet information about a specific volume."
            | FolderInfo _ -> "Usage: volinfo folderinfo <folder>\n\nGet information about a specific folder."
            | Help -> "Usage: volinfo <command>\n\nCommands:\n  listvolumes\n  volumeinfo\n\nUse 'volinfo help <command>' for more information about a command."

[<EntryPoint>]
let main argv = 
    let parser = ArgumentParser.Create<Arguments>("volinfo")
    let results = parser.ParseCommandLine argv
    
    let results = results.GetAllResults()

    if results.Length = 0 then
        parser.PrintUsage() |> printfn "%s"
    else
        match results[0] with
        | ListVolumes ->
            volumes()
            |> printf "%A"
        | VolumeInfo volume ->
            volumeInfo volume
            |> printf "%A"
        | FolderInfo folder ->
            Folder.folderInfo folder
            |> printf "%A"
        | Help -> parser.PrintUsage() |> printfn "%s"

    0
