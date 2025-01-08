using System;

namespace MSCaddie.Shared.Dtos
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string? DataValue { get; set; }
        public string? SystemType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string? Description { get; set; }
    }
}
