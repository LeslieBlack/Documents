----------------------------------------------------------------------
-- Creates an extract of all stock in HS RTNS bins for WH2			--
-- RK	25/05/2018	Initial version									--
-- PC	06/06/2018	Amendments as requested by Whishworks			--
--																	--
-- Instructions:													--
-- Insert the correct RTN bin number into the LIKE clause.  Run the	--
-- query and copy the results WITH headers (Ctrl+Shift+C) into a	--
-- CSV file.  Format the barcode column (K) as a number with zero	--
-- decimal places.  Each file should be no more than ~1,500 rows	--
-- to prevent the XML conversion from failing.						--
----------------------------------------------------------------------

SELECT
	*

FROM
	OPENQUERY
		(CIMSF
,		'SELECT
			l.loc_sort											AS "loc_sort"
,			st.gstock_user_type									AS "gstock_user_type"
,			gs.bin												AS "bin"
,			gs.season											AS "season"
,			gs.sty_num											AS "sty_num"
,			gs.sty_qual											AS "sty_qual"
,			gs.sty_size											AS "sty_size"
,			gs.bf_mat_char_val									AS "bf_mat_char_val"
,			sd.sty_name											AS "sty_name"
,			sb.wsty_size										AS "size"
,			sb.sty_barcode										AS "barcode"
,			DECODE(ts.size_num,
				1, gs.qty1,		2, gs.qty2,		3, gs.qty3,
				4, gs.qty4,		5, gs.qty5,		6, gs.qty6,
				7, gs.qty7,		8, gs.qty8,		9, gs.qty9,
				10, gs.qty10,	11, gs.qty11,	12, gs.qty12)	AS "qty"

		FROM
			pro.gar_bin_stk			gs
,			pro.loc_defs			l
,			pro.val_gstock_types	st
,			pro.sty_defs			sd
,			pro.sty_barcodes		sb
,			pro.val_sty_types		vst
,			pro.twelve_sizes		ts

		WHERE
			gs.loc_num			= l.loc_num
AND			gs.gstock_type		= st.gstock_type
AND			gs.season			= sd.season
AND			gs.sty_num			= sd.sty_num
AND			gs.sty_qual			= sd.sty_qual
AND			st.gstock_user_type	= ''HS RTNS''
AND			gs.bin				LIKE ''RTN5''
AND			gs.bf_mat_char_val	= sb.bf_mat_char_val
AND			sd.sty_code			= sb.sty_id
AND			sd.sty_type			= vst.sty_type
AND			sb.wsty_size		= DECODE(ts.size_num,
									1, vst.size1,	2,  vst.size2,	3, vst.size3,
									4, vst.size4,	5,  vst.size5,	6, vst.size6,
									7, vst.size7,	8,  vst.size8,	9, vst.size9,
									10, vst.size10,	11, vst.size11,	12, vst.size12)
AND							DECODE(ts.size_num,
									1, gs.qty1,		2, gs.qty2,		3, gs.qty3,
									4, gs.qty4,		5, gs.qty5,		6, gs.qty6,
									7, gs.qty7,		8, gs.qty8,		9, gs.qty9,
									10, gs.qty10,	11, gs.qty11,	12, gs.qty12) IS NOT NULL  
AND			l.loc_sort			= ''WH2''

		ORDER BY
			gs.bin				ASC
,			gs.season			ASC
,			gs.sty_num			ASC
,			gs.sty_qual			ASC
,			gs.sty_size			ASC
,			gs.bf_mat_char_val	ASC')
