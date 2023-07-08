package com.example.arhomedesign;

import java.io.Serializable;

// model class for furnitures
public class Furnitures implements Serializable {
    public int furnitureId ;
    public String type;
    public String color;
    public String style;
    public String brand1;
    public String phoneNumber;
    public String address;
    public String logo;
    public String location;
    public String picture;

    public Furnitures(int furnitureId, String type, String color, String style, String brand1, String phoneNumber, String address, String logo, String location, String picture){
        this.furnitureId = furnitureId;
        this.type = type;
        this.color = color;
        this.style = style;
        this.brand1 = brand1;
        this.phoneNumber = phoneNumber;
        this.address = address;
        this.logo = logo;
        this.location = location;
        this.picture = picture;
    }

}
