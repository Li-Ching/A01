package com.example.arhomedesign.utils;

import android.os.Parcel;
import android.os.Parcelable;

import androidx.annotation.NonNull;

public class furnitures implements Parcelable {

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

    protected furnitures(Parcel in) {
        if (in.readByte() == 0) {
            furnitureId = null;
        } else {
            furnitureId = in.readInt();
        }
        furnitureName = in.readString();
        type = in.readString();
        color = in.readString();
        style = in.readString();
        brand1 = in.readString();
        phoneNumber = in.readString();
        address = in.readString();
        logo = in.readString();
        location = in.readString();
        picture = in.readString();
    }

    public static final Creator<furnitures> CREATOR = new Creator<furnitures>() {
        @Override
        public furnitures createFromParcel(Parcel in) {
            return new furnitures(in);
        }

        @Override
        public furnitures[] newArray(int size) {
            return new furnitures[size];
        }
    };


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

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(@NonNull Parcel dest, int flags) {
        if (furnitureId == null) {
            dest.writeByte((byte) 0);
        } else {
            dest.writeByte((byte) 1);
            dest.writeInt(furnitureId);
        }
        dest.writeString(furnitureName);
        dest.writeString(type);
        dest.writeString(color);
        dest.writeString(style);
        dest.writeString(brand1);
        dest.writeString(phoneNumber);
        dest.writeString(address);
        dest.writeString(logo);
        dest.writeString(location);
        dest.writeString(picture);
    }
}