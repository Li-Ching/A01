package com.example.test1;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {
    private ListView listView;
    private List<furnitures> furnitureList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        listView = findViewById(R.id.listviewData);
        furnitureList = new ArrayList<>();

        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
        Call<List<furnitures>> call = methods.getFurnitures();
        call.enqueue(new Callback<List<furnitures>>() {
            @Override
            public void onResponse(Call<List<furnitures>> call, Response<List<furnitures>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    furnitureList = response.body();
                    String[] names = new String[furnitureList.size()];
                    int i = 0;
                    for (furnitures item : furnitureList) {
                        names[i]="FurnitureId : " + item.getFurnitureId() + "\nType : " + item.getType() + "\ncolor : " + item.getColor()
                                + "\nStyle : " + item.getStyle() + "\nBrand : " + item.getBrand1() + "\nPhoneNumber : "
                                + item.getPhoneNumber() +"\nAddress : " + item.getAddress();
                        i+=1;
                    }
                    listView.setAdapter(new ArrayAdapter<String>(getApplicationContext(), android.R.layout.simple_list_item_1, names));
                } else {
                    Toast.makeText(getApplicationContext(), "An error has occurred", Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<furnitures>> call, Throwable t) {
                Toast.makeText(getApplicationContext(), "An error has occurred", Toast.LENGTH_LONG).show();
            }
        });
    }
}