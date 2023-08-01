package com.example.arhomedesign;

import android.os.Bundle;

import androidx.core.view.ViewCompat;
import androidx.core.widget.NestedScrollView;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;

public class SofaFragment extends Fragment {

    LinearLayout sofa1, sofa2;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_sofa, container, false);

        sofa1 = view.findViewById(R.id.butSofa1);
        sofa2 = view.findViewById(R.id.butSofa2);

        sofa1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Sofa_1Fragment sofa1Fragment = new Sofa_1Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, sofa1Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        sofa2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Sofa_2Fragment sofa2Fragment = new Sofa_2Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, sofa2Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        return view;
    }
}