package com.example.test1;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {
    private Button btnGetData;
    private ListView listView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        btnGetData = findViewById(R.id.btnGetData);
        listView = findViewById(R.id.listviewData);
        btnGetData.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
                Call <List<furnitures>> call = methods.getFurnitures();
                call.enqueue(new Callback <List<furnitures>> () {
                    @Override
                    public void onResponse(Call <List<furnitures>> call, Response <List<furnitures>> response) {
                        String[] names = new String[10];
                        int i = 0;
                        for (furnitures item : response.body()) {
                            names[i]="FurnitureId : " + item.getFurnitureId() + "\nType : " + item.getType() + "\ncolor : " + item.getColor()
                                    + "\nStyle : " + item.getStyle() + "\nBrand : " + item.getBrand1() + "\nPhoneNumber : "
                                    + item.getPhoneNumber() +"\nAddress : " + item.getAddress();
                            i+=1;
                        }
                        listView.setAdapter(new ArrayAdapter < String > (getApplicationContext(), android.R.layout.simple_list_item_1, names));
                    }
                    @Override
                    public void onFailure(Call <List<furnitures>> call, Throwable t) {
                        Toast.makeText(getApplicationContext(), "An error has occured", Toast.LENGTH_LONG).show();
                    }
                });
            }
        });
    }
}