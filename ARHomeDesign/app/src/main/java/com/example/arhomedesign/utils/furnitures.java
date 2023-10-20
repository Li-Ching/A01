package com.example.arhomedesign.utils;

public class furnitures {

    private Integer furnitureId;

    private String furnitureName;
    private String type;
    private String color;
    private String style;
    private String brand1;
    private String phoneNumber;
    private String address;
    private String logo;
    private String location;
    private String picture;

    public furnitures(Integer furnitureId, String furnitureName, String type, String color, String style, String brand1, String phoneNumber, String address, String logo, String location, String picture) {
        this.furnitureId = furnitureId;
        this.furnitureName = furnitureName;
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

    public Integer getFurnitureId() {
        return furnitureId;
    }

    public void setFurnitureId(Integer furnitureId) {
        this.furnitureId = furnitureId;
    }

    public String getFurnitureName() {
        return furnitureName;
    }

    public void setFurnitureName(String furnitureName) {
        this.furnitureName = furnitureName;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public String getStyle() {
        return style;
    }

    public void setStyle(String style) {
        this.style = style;
    }

    public String getBrand1() {
        return brand1;
    }

    public void setBrand1(String brand1) {
        this.brand1 = brand1;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getLogo() {
        return logo;
    }

    public void setLogo(String logo) {
        this.logo = logo;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getPicture() {
        return picture;
    }

    public void setPicture(String picture) {
        this.picture = picture;
    }
}