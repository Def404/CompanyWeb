﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompanyApi.Models.Mongo;

public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("fullName")]
    public string FullName { get; set; } = null!;
    
    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = null!;
}