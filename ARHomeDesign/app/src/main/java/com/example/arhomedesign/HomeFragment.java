package com.example.arhomedesign;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;

public class HomeFragment extends Fragment {
    LinearLayout table, chair, sofa, bed;
    LinearLayout table2, bed2, table3, sofa1; //recommendation part

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        table = view.findViewById(R.id.catTable);
        chair = view.findViewById(R.id.catChair);
        sofa = view.findViewById(R.id.catSofa);
        bed = view.findViewById(R.id.catBed);

        table2 = view.findViewById(R.id.butTable2);
        bed2 = view.findViewById(R.id.butBed2);
        table3 = view.findViewById(R.id.butTable3);
        sofa1 = view.findViewById(R.id.butSofa1);


        table.setOnClickListener(new View.OnClickListener() {
            @Override
            
            public void onClick(View v) {
                TableFragment tableFragment = new TableFragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_in, R.anim.fade_out, R.anim.fade_in, R.anim.slide_out);
                transaction.replace(R.id.FrameLayout, tableFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        chair.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ChairFragment chairFragment = new ChairFragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_in, R.anim.fade_out, R.anim.fade_in, R.anim.slide_out);
                transaction.replace(R.id.FrameLayout, chairFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        sofa.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SofaFragment sofaFragment = new SofaFragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_in, R.anim.fade_out, R.anim.fade_in, R.anim.slide_out);
                transaction.replace(R.id.FrameLayout, sofaFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        bed.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                BedFragment bedFragment = new BedFragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_in, R.anim.fade_out, R.anim.fade_in, R.anim.slide_out);
                transaction.replace(R.id.FrameLayout, bedFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        table2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Table_2Fragment table_2Fragment = new Table_2Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, table_2Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        bed2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Bed_2Fragment bed2Fragment = new Bed_2Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, bed2Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        table3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Table_3Fragment table_3Fragment = new Table_3Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, table_3Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        sofa1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Sofa_1Fragment sofa_1Fragment = new Sofa_1Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, sofa_1Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        return view;
    }


}