module Folder
open System.IO
open System

type FolderInfo = {
    Name: string
    Exists: bool
    Root: DirectoryInfo
    Parent: DirectoryInfo
    CreationTime: DateTime
    LastAccessTime: DateTime
    LastWriteTime: DateTime
    Attributes: FileAttributes
    FullName: string
    Files: FileInfo[]
    Folders: FolderInfo[]
    Size: int64
    SizeMb: int64
}

let rec folderInfo (folderName:string) =
    let dirInfo = 
        DirectoryInfo(folderName)

    match dirInfo with
    | folder when folder.Exists=false -> None
    | folder -> 
        let files = folder.GetFiles()
        let directories = 
            folder.GetDirectories()
            |> Array.map (fun d -> folderInfo d.FullName)
            |> Array.choose id

        let size = 
            files
            |> Array.sumBy (fun f -> f.Length)  
            |> (+) (directories |> Array.sumBy (fun d -> d.Size))

        { Name = dirInfo.Name
          Exists = dirInfo.Exists
          Root = dirInfo.Root
          Parent = dirInfo.Parent
          CreationTime = dirInfo.CreationTime
          LastAccessTime = dirInfo.LastAccessTime
          LastWriteTime = dirInfo.LastWriteTime
          Attributes = dirInfo.Attributes
          FullName = dirInfo.FullName
          Files = files
          Folders = directories
          Size = size 
          SizeMb = size / (1024L*1024L)} |> Some