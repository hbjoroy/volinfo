module Volume

let volumes () =
    let volumes = System.IO.DriveInfo.GetDrives()
    volumes
    |> Array.map (fun volume -> volume.Name)

type VolumeInfo = {
    Name: string
    DriveType: System.IO.DriveType
    DriveFormat: string
    AvailableFreeSpace: int64
    TotalSize: int64
    IsReady: bool
    RootDirectory: System.IO.DirectoryInfo
    VolumeLabel: string
}

let volumeInfo (volumeName:string) =
    let matchngVolumes =
        System.IO.DriveInfo.GetDrives()
        |> Array.filter (fun v -> v.Name.ToLower()  = volumeName.ToLower() )
    match matchngVolumes with
    | [|volume|] -> 
            { Name = volume.Name
              DriveType = volume.DriveType
              DriveFormat = volume.DriveFormat
              AvailableFreeSpace = volume.AvailableFreeSpace
              TotalSize = volume.TotalSize 
              IsReady = volume.IsReady
              RootDirectory = volume.RootDirectory
              VolumeLabel = volume.VolumeLabel } |> Some
    | _ -> None
