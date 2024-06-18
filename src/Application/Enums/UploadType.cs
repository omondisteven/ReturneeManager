using System.ComponentModel;

namespace ReturneeManager.Application.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\Products")]
        Product,

        [Description(@"Images\Persons")]
        Person,

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document
    }
}