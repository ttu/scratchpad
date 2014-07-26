;; Anything you type in here will be executed
;; immediately with the results shown on the
;; right.

(+ 1 2)
(+ 1 (* 2 4) 3)


'("hello " "devdays")

(list 1 (+ 1 1) 3)
'(list 1 (+ 1 1) 3)

["hello " "devday"]
(vec '(1 2 3))

(hash-map "a" 1 "b" 2)
{"a" 1 "b" 2}
{"a",1,"b",2}

(def my-best-value 14)
my-best-value

(def my-best-list '(1 2 3 4 5))
my-best-list

(def my-best-vector [1 2 3 4 5])
my-best-vector

(def my-map {"a" 1 "b" 2})
(my-map "b")

(def my-other-map {:a 1 :b 2 :c {:d 3}})
(:b my-other-map)
(:d (:c my-other-map))

(keys my-map)
(vals my-map)

(def s [1 2 3 4])

(first s)
(second s)
(nth s 3)

(reverse s)
(rest s)

(first (reverse s))
(second (reverse s))
((comp second reverse) s)
((comp peek pop vec) s)

(fn [x] (* x x))

((fn [x]
  (* x x))
  4)

(def sqr (fn [x] (* x x)))
(sqr 4)

(defn sqr-2 [x]
  (* x x))

(sqr-2 4)

(map sqr [1 2 3 4 5])
(map inc [1 2 3 4 5])

(map (fn [x] (+ 3 x)) [1 2 3 4 5])
(map #(+ 3 %) [1 2 3 4 5])

(map #(+ %1 %2) [1 2 3 4 5] [3 4 5 6 7])

(filter even? [1 2 3 4 5])
(remove even? [ 1 2 3 4 5])

(map #(+ 5 %) [1 2 3 4 5])

(reduce #(+ %1 %2) [1 2 3 4 5])
(reduce + [1 2 3 4 5])
(reduce + 5 [1 2 3 4 5])

(defn my-plus [memo x]
  (+ memo x))

(reduce my-plus 5 [1 2 3 4 5])
(reduce my-plus [1 2 3 4 5])

(apply + [1 2 3 4 5])
(+ 1 2 3 4 5)


(if (even? 2)
  "It's even!"
  "Its odd!")

(when (even? 3)
  "YEAH!")


(defn pos-or-neq [x]
  (cond
    (< 0 x) "positive"
    (> 0 x) "negative"
   :else "It must be zero!"))

(pos-or-neq 1)
(pos-or-neq -1)
(pos-or-neq 0)

(range 10)
(range 4 10)
(partition 5 (range 15))
(partition 5 1 (range 15))
(partition-all 5 (range 15))

(.toString 2)
(type 2)

(condp = (type 2)
  java.lang.Long "Integer"
  java.lang.String "String"
  :else "Neither")

'"www.github.com/jrosti/ontrail"






































