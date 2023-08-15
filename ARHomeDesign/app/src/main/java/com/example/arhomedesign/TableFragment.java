package com.example.arhomedesign;

import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;

public class TableFragment extends Fragment {

    LinearLayout table1, table2, table3;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_table, container, false);

        table1 = view.findViewById(R.id.butTable1);
        table2 = view.findViewById(R.id.butTable2);
        table3 = view.findViewById(R.id.butTable3);


        table1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), FurnitureActivity.class);
                startActivity(intent);
            }
        });

        table2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Table_2Fragment table2Fragment = new Table_2Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, table2Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        table3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Table_3Fragment table3Fragment = new Table_3Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, table3Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        return view;
    }
}