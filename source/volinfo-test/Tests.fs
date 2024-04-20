module Tests

open System
open Xunit
open System.IO

[<Fact>]
let ``My test`` () =

    let result = Volume.volumeInfo "C:\\"
    match result with
    | Some volumeInfo -> 
        Assert.Equal("C:\\", volumeInfo.Name)
        Assert.Equal(DriveType.Fixed, volumeInfo.DriveType)
        Assert.Equal("NTFS", volumeInfo.DriveFormat)
        Assert.True(volumeInfo.AvailableFreeSpace > 0L)
        Assert.True(volumeInfo.TotalSize > 0L)
        Assert.True(volumeInfo.IsReady)
        Assert.NotNull(volumeInfo.RootDirectory)
        Assert.NotNull(volumeInfo.VolumeLabel)
    | None -> Assert.True(false)

[<Fact>]
let ``My test 2 `` () =

    let result = Folder.folderInfo "C:\\"
    match result with
    | Some folderInfo -> 
        Assert.Equal("C:\\", folderInfo.Name)
        Assert.True(folderInfo.Exists)
        Assert.NotNull(folderInfo.Root)
        //Assert.NotNull(folderInfo.Parent)
        Assert.NotNull(folderInfo.CreationTime)
        Assert.NotNull(folderInfo.LastAccessTime)
        Assert.NotNull(folderInfo.LastWriteTime)
        Assert.NotNull(folderInfo.Attributes)
        Assert.Equal("C:\\", folderInfo.FullName)
    | None -> Assert.True(false)
[<Fact>]
let ``My test 2 1`` () =

    let result = Folder.folderInfo "does not exist"
    match result with
    | Some _ -> 
        Assert.Fail("Should not have found a folder")
    | None -> 
        Assert.True(true)


[<Fact>]
let ``My test 3 `` () =

    let result = Volume.volumes()
    Assert.True(result.Length > 0)
    Assert.True(Array.contains "C:\\" result)

